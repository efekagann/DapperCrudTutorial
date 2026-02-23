using DapperCrudTutorial.Data;
using DapperCrudTutorial.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DapperCrudTutorial.Pages.Heroes
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ISuperHeroRepository _repository;

        public IndexModel(ISuperHeroRepository repository)
        {
            _repository = repository;
        }

        public IList<SuperHero> Heroes { get; set; } = new List<SuperHero>();

        public async Task OnGetAsync()
        {
            Heroes = (await _repository.GetAllAsync()).ToList();
        }
    }
}
