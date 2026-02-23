using DapperCrudTutorial.Data;
using DapperCrudTutorial.Models;
using Microsoft.AspNetCore.Mvc;

namespace DapperCrudTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ISuperHeroRepository _repository;

        public SuperHeroController(ISuperHeroRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuperHero>>> GetAllSuperHeroes()
        {
            var heroes = await _repository.GetAllAsync();
            return Ok(heroes);
        }

        [HttpGet("{heroId}")]
        public async Task<ActionResult<SuperHero>> GetHero(int heroId)
        {
            var hero = await _repository.GetByIdAsync(heroId);
            if (hero is null)
                return NotFound();
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<SuperHero>>> CreateHero([FromBody] SuperHero hero)
        {
            await _repository.CreateAsync(hero);
            return Ok(await _repository.GetAllAsync());
        }

        [HttpPut]
        public async Task<ActionResult<IEnumerable<SuperHero>>> UpdateHero([FromBody] SuperHero hero)
        {
            await _repository.UpdateAsync(hero);
            return Ok(await _repository.GetAllAsync());
        }

        [HttpDelete("{heroId}")]
        public async Task<ActionResult<IEnumerable<SuperHero>>> RemoveHero(int heroId)
        {
            await _repository.DeleteAsync(heroId);
            return Ok(await _repository.GetAllAsync());
        }
    }
}
