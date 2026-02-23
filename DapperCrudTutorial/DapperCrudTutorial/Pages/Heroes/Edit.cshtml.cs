using DapperCrudTutorial.Data;
using DapperCrudTutorial.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DapperCrudTutorial.Pages.Heroes
{
    public class EditModel : PageModel
    {
        private readonly ISuperHeroRepository _repository;

        public EditModel(ISuperHeroRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public SuperHero Hero { get; set; } = new SuperHero();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var hero = await _repository.GetByIdAsync(id);
            if (hero is null)
                return NotFound();

            Hero = hero;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _repository.UpdateAsync(Hero);
            return RedirectToPage("Index");
        }
    }
}
