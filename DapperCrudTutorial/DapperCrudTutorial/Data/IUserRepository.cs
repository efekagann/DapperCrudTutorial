using DapperCrudTutorial.Models;

namespace DapperCrudTutorial.Data
{
    public interface IUserRepository
    {
        Task<AppUser?> GetByUsernameAsync(string username);
        Task CreateAsync(AppUser user);
        Task<bool> AnyUserExistsAsync();
    }
}
