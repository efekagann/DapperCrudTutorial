using Dapper;
using DapperCrudTutorial.Models;
using System.Data.SqlClient;

namespace DapperCrudTutorial.Data
{
    public class SuperHeroRepository : ISuperHeroRepository
    {
        private readonly IConfiguration _config;

        public SuperHeroRepository(IConfiguration config)
        {
            _config = config;
        }

        private SqlConnection CreateConnection() =>
            new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        public async Task<IEnumerable<SuperHero>> GetAllAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<SuperHero>("SELECT * FROM superheroes");
        }

        public async Task<SuperHero?> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<SuperHero>(
                "SELECT * FROM superheroes WHERE id = @Id", new { Id = id });
        }

        public async Task CreateAsync(SuperHero hero)
        {
            using var connection = CreateConnection();
            await connection.ExecuteAsync(
                "INSERT INTO superheroes (name, firstname, lastname, place) VALUES (@Name, @FirstName, @LastName, @Place)",
                hero);
        }

        public async Task UpdateAsync(SuperHero hero)
        {
            using var connection = CreateConnection();
            await connection.ExecuteAsync(
                "UPDATE superheroes SET name = @Name, firstname = @FirstName, lastname = @LastName, place = @Place WHERE id = @Id",
                hero);
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = CreateConnection();
            await connection.ExecuteAsync(
                "DELETE FROM superheroes WHERE id = @Id", new { Id = id });
        }
    }
}
