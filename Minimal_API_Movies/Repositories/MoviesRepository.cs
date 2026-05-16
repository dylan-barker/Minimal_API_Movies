using Dapper;
using Microsoft.Data.SqlClient;
using Minimal_API_Movies.DTOs;
using Minimal_API_Movies.Entities;
using System.Data;

namespace Minimal_API_Movies.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly string connectionString;
        private readonly HttpContext httpContext;

        public MoviesRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection")!;
            httpContext = httpContextAccessor.HttpContext!;
        }

        public async Task<int> Create(Movie movie)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var id = await connection.QuerySingleAsync<int>(
                    "Movies_Create",
                    new { movie.Title, movie.InTheaters, movie.ReleaseDate, movie.Poster },
                    commandType: CommandType.StoredProcedure);
                movie.Id = id;
                return id;
            }
        }

        public async Task<List<Movie>> GetAll(PaginationDTO pagination)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var movies = await connection.QueryAsync<Movie>("Movies_GetAll",
                    new { pagination.Page, pagination.RecordsPerPage },
                    commandType: CommandType.StoredProcedure);

                var MovieCount = await connection.QuerySingleAsync<int>("Movies_Count",
                    commandType: CommandType.StoredProcedure);

                httpContext.Response.Headers.Append("totalAmountOfRecords",
                    MovieCount.ToString());

                return movies.ToList();
            }
        }

        public async Task<Movie?> GetById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var movie = await connection.QueryFirstOrDefaultAsync<Movie>(
                    "Movies_GetById",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
                return movie;
            }
        }

        public async Task<bool> Exists(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var exists = await connection.QuerySingleAsync<bool>(
                    "Movies_Exists",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
                return exists;
            }
        }

        public async Task Update(Movie movie)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(
                    "Movies_Update",
                    new { movie.Id, movie.Title, movie.InTheaters, movie.ReleaseDate, movie.Poster },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(
                    "Movies_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
