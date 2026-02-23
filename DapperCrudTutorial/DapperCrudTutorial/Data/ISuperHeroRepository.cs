using DapperCrudTutorial.Models;

namespace DapperCrudTutorial.Data
{
    public interface ISuperHeroRepository
    {
        Task<IEnumerable<SuperHero>> GetAllAsync();
        Task<SuperHero?> GetByIdAsync(int id);
        Task CreateAsync(SuperHero hero);
        Task UpdateAsync(SuperHero hero);
        Task DeleteAsync(int id);
    }
}
