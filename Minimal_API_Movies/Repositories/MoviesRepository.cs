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
                using (var multi = await connection.QueryMultipleAsync(
                    "Movies_GetById",
                    new { id },
                    commandType: CommandType.StoredProcedure))
                {
                    var movie = await multi.ReadFirstAsync<Movie>();
                    var comments = await multi.ReadAsync<Comment>();
                    var genres = await multi.ReadAsync<Genre>();
                    var actors = await multi.ReadAsync<ActorMovie>();
                    movie.Comments = comments.ToList();
                    foreach (var genre in genres)
                    {
                        movie.GenresMovies.Add(new GenreMovie
                        {
                            GenreId = genre.Id,
                            Genre = genre
                        });
                    }
                    foreach (var actor in actors)
                    {
                        movie.ActorsMovies.Add(new ActorMovie
                        {
                            ActorId = actor.ActorId,
                            Character = actor.Character,
                            Actor = new Actor { Name = actor.Name },
                            Order = actor.Order
                        });
                    }
                    return movie;
                }
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

        public async Task Assign(int id, List<int> genresIds)
        {
            var dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));

            foreach (var genreId in genresIds)
            {
                dt.Rows.Add(genreId);
            }

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(
                    "Movies_AssignGenres",
                    new { MovieId = id, GenresIds = dt },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Assign(int id, List<ActorMovie> actors)
        {
            for (int i = 1; i <= actors.Count; i++)
            {
                actors[i - 1].Order = i;
            }

            var dt = new DataTable();
            dt.Columns.Add("ActorId", typeof(int));
            dt.Columns.Add("Character", typeof(string));
            dt.Columns.Add("Order", typeof(int));

            foreach (var actorMovie in actors)
            {
                dt.Rows.Add(actorMovie.ActorId, actorMovie.Character, actorMovie.Order);
            }

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(
                    "Movies_AssignActors",
                    new { MovieId = id, Actors = dt },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<Movie>> Filter(MoviesFilterDTO moviesFilterDTO)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var movies = connection.Query<Movie>(
                    "Movies_Filter",
                    new { 
                        moviesFilterDTO.Page,
                        moviesFilterDTO.RecordsPerPage,
                        moviesFilterDTO.Title,
                        moviesFilterDTO.GenreId,
                        moviesFilterDTO.FutureReleases,
                        moviesFilterDTO.InTheaters,
                        moviesFilterDTO.OrderByField,
                        moviesFilterDTO.OrderByAscending
                    },
                    commandType: CommandType.StoredProcedure);

                var moviesCount = connection.QuerySingle<int>(
                    "Movies_Count",
                    new
                    {
                        moviesFilterDTO.Title,
                        moviesFilterDTO.GenreId,
                        moviesFilterDTO.FutureReleases,
                        moviesFilterDTO.InTheaters
                    },
                    commandType: CommandType.StoredProcedure);
                
                httpContext.Response.Headers.Append("totalAmountOfRecords", moviesCount.ToString());
                
                return movies.ToList();
            }
        }
    }
}