using Dapper;
using DapperCrudTutorial.Models;
using System.Data.SqlClient;

namespace DapperCrudTutorial.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _config;

        public UserRepository(IConfiguration config)
        {
            _config = config;
        }

        private SqlConnection CreateConnection() =>
            new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        public async Task<AppUser?> GetByUsernameAsync(string username)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<AppUser>(
                "SELECT * FROM Users WHERE Username = @Username",
                new { Username = username });
        }

        public async Task CreateAsync(AppUser user)
        {
            using var connection = CreateConnection();
            await connection.ExecuteAsync(
                "INSERT INTO Users (Username, Email, PasswordHash) VALUES (@Username, @Email, @PasswordHash)",
                new { user.Username, Email = user.Email ?? string.Empty, user.PasswordHash });
        }

        public async Task<bool> AnyUserExistsAsync()
        {
            using var connection = CreateConnection();
            var count = await connection.ExecuteScalarAsync<int>("SELECT COUNT(1) FROM Users");
            return count > 0;
        }
    }
}
