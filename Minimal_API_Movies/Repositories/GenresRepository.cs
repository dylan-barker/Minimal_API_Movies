using Dapper;
using Microsoft.Data.SqlClient;
using Minimal_API_Movies.Entities;
using System.Data;

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
                var procedure = "Genres_Create";
                var id = await connection.QuerySingleAsync<int>(procedure, new { genre.Name }, commandType: CommandType.StoredProcedure);
                genre.Id = id;
                return id;
            }
        }

        public async Task Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "Genres_Delete";
                await connection.ExecuteAsync(procedure, new { id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<bool> Exists(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "Genres_Exist";
                var exists = await connection.QuerySingleAsync<bool>(procedure, new { id }, commandType: CommandType.StoredProcedure);
                return exists;
            }
        }

        public async Task<List<Genre>> GetAll()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "Genres_GetAll";
                var genres = await connection.QueryAsync<Genre>(procedure, commandType: CommandType.StoredProcedure);
                return genres.ToList();
            }
        }

        public async Task<Genre?> GetById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "Genres_GetById";
                var genre = await connection.QuerySingleOrDefaultAsync<Genre>(procedure, new { Id = id }, commandType: CommandType.StoredProcedure);
                return genre;
            }
        }

        public async Task Update(Genre genre)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "Genres_Update";
                await connection.ExecuteAsync(procedure, new { genre.Id, genre.Name }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
