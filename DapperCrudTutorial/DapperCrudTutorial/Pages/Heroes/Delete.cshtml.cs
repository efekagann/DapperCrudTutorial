using DapperCrudTutorial.Data;
using DapperCrudTutorial.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DapperCrudTutorial.Pages.Heroes
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly ISuperHeroRepository _repository;

        public DeleteModel(ISuperHeroRepository repository)
        {
            _repository = repository;
        }

        public SuperHero Hero { get; set; } = new SuperHero();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var hero = await _repository.GetByIdAsync(id);
            if (hero is null)
                return NotFound();

            Hero = hero;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToPage("Index");
        }
    }
}
