using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Minimal_API_Movies.Entities;
using System.Data.Common;

namespace Minimal_API_Movies.Repositories
{
    public class GenresRepository : IGenresRepository
    {
        private readonly string connectionString;

        public GenresRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        public async Task<int> Create(Genre genre)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "INSERT INTO Genres (Name) VALUES (@Name); SELECT SCOPE_IDENTITY();";
                var id = await connection.QuerySingleAsync<int>(query, genre);
                genre.Id = id;
                return id;
            }
        }

        public async Task<List<Genre>> GetAll()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "SELECT Id, Name FROM Genres ORDER BY Name";
                var genres = await connection.QueryAsync<Genre>(query);
                return genres.ToList();
            }
        }

        public async Task<Genre?> GetById(int id)
        {
            using(var connection = new SqlConnection(connectionString))
            {
                var query = "SELECT Id, Name FROM Genres WHERE Id = @Id";
                var genre = await connection.QuerySingleOrDefaultAsync<Genre>(query, new { Id = id });
                return genre;
            }
        }
    }
}
