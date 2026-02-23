using DapperCrudTutorial.Data;
using DapperCrudTutorial.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DapperCrudTutorial.Pages.Heroes
{
    public class CreateModel : PageModel
    {
        private readonly ISuperHeroRepository _repository;

        public CreateModel(ISuperHeroRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public SuperHero Hero { get; set; } = new SuperHero();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _repository.CreateAsync(Hero);
            return RedirectToPage("Index");
        }
    }
}
