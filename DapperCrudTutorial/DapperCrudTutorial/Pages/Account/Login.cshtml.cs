using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using DapperCrudTutorial.Data;
using DapperCrudTutorial.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DapperCrudTutorial.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<AppUser> _hasher;

        public LoginModel(IUserRepository userRepository, IPasswordHasher<AppUser> hasher)
        {
            _userRepository = userRepository;
            _hasher = hasher;
        }

        [BindProperty]
        public LoginInput Input { get; set; } = new();

        public string? ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToPage("/Heroes/Index");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = await _userRepository.GetByUsernameAsync(Input.Username);
            if (user is null)
            {
                ErrorMessage = "Kullanıcı adı veya şifre hatalı.";
                return Page();
            }

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, Input.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                ErrorMessage = "Kullanıcı adı veya şifre hatalı.";
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToPage("/Heroes/Index");
        }
    }

    public class LoginInput
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
