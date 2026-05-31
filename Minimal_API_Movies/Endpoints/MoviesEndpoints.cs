using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Minimal_API_Movies.DTOs;
using Minimal_API_Movies.Entities;
using Minimal_API_Movies.Filters;
using Minimal_API_Movies.Repositories;
using Minimal_API_Movies.Services;

namespace Minimal_API_Movies.Endpoints
{
    public static class MoviesEndpoints
    {
        private readonly static string container = "movies";
        public static RouteGroupBuilder MapMovies(this RouteGroupBuilder group)
        {
            group.MapPost("/", Create).DisableAntiforgery()
                .AddEndpointFilter<ValidationFilter<CreateMovieDTO>>()
                .RequireAuthorization("isadmin")
                .WithOpenApi();

            group.MapGet("/with-actors", GetAll)
                .CacheOutput(c => c.Expire(TimeSpan.FromMinutes(1))).WithTags("movies-get");

            group.MapGet("/{id:int}", GetById);

            group.MapPut("/{id:int}", Update)
                .DisableAntiforgery()
                .AddEndpointFilter<ValidationFilter<CreateMovieDTO>>()
                .RequireAuthorization("isadmin")
                .WithOpenApi();

            group.MapDelete("/{id:int}", Delete)
                .RequireAuthorization("isadmin");

            group.MapPost("/{id:int}/genres", AssignGenres)
                .RequireAuthorization("isadmin")
                .WithOpenApi();

            group.MapPost("/{id:int}/actors", AssignActors)
                .RequireAuthorization("isadmin")
                .WithOpenApi();
            return group;
        }

        static async Task<Ok<List<MovieDTO>>> GetAll(IMapper mapper, IMoviesRepository moviesRepository,
            int page = 1, int recordsPerPage = 10)
        {
            var pagination = new PaginationDTO { Page = page, RecordsPerPage = recordsPerPage };
            var movies = await moviesRepository.GetAll(pagination);
            var moviesDTO = mapper.Map<List<MovieDTO>>(movies);
            return TypedResults.Ok(moviesDTO);
        }

        static async Task<Results<Ok<MovieDTO>, NotFound>> GetById(int id, IMapper mapper,
            IMoviesRepository moviesRepository)
        {
            var movie = await moviesRepository.GetById(id);
            if (movie is null)
            {
                return TypedResults.NotFound();
            }
            var movieDTO = mapper.Map<MovieDTO>(movie);
            return TypedResults.Ok(movieDTO);
        }

        static async Task<Created<MovieDTO>> Create([FromForm] CreateMovieDTO createMovieDto,
            IFileStorage fileStorage, IOutputCacheStore outputCacheStore, IMapper mapper,
            IMoviesRepository moviesRepository)
        {
            var movie = mapper.Map<Movie>(createMovieDto);

            if (createMovieDto.Poster != null)
            {
                var url = await fileStorage.Store(container, createMovieDto.Poster);
                movie.Poster = url;
            }

            var id = await moviesRepository.Create(movie);
            await outputCacheStore.EvictByTagAsync("movies-get", default);
            var movieDTO = mapper.Map<MovieDTO>(movie);
            return TypedResults.Created($"/movies/{movie.Id}", movieDTO);
        }

        static async Task<Results<Ok<MovieDTO>, NotFound, NoContent>> Update(int id, [FromForm] CreateMovieDTO createMovieDto,
            IFileStorage fileStorage, IOutputCacheStore outputCacheStore, IMapper mapper,
            IMoviesRepository moviesRepository)
        {
            var movie = await moviesRepository.GetById(id);
            if (movie is null)
            {
                return TypedResults.NotFound();
            }

            var movieForUpdate = mapper.Map<Movie>(createMovieDto);
            movieForUpdate.Id = id;
            movieForUpdate.Poster = movie.Poster;
            if (createMovieDto.Poster is not null)
            {
                var url = await fileStorage.Edit(movieForUpdate.Poster, container, createMovieDto.Poster);
                movieForUpdate.Poster = url;
            }

            await moviesRepository.Update(movieForUpdate);
            await outputCacheStore.EvictByTagAsync("movies-get", default);
            return TypedResults.NoContent();
        }

        static async Task<Results<NoContent, NotFound>> Delete(int id, IFileStorage fileStorage,
            IOutputCacheStore outputCacheStore, IMapper mapper, IMoviesRepository moviesRepository)
        {
            var movie = await moviesRepository.GetById(id);
            if (movie is null)
            {
                return TypedResults.NotFound();
            }

            await moviesRepository.Delete(id);
            await fileStorage.Delete(movie.Poster, container);
            await outputCacheStore.EvictByTagAsync("movies-get", default);
            return TypedResults.NoContent();
        }

        static async Task<Results<NoContent, NotFound, BadRequest<string>>> AssignGenres(int id, List<int> genreIds,
            IMoviesRepository moviesRepository, IGenresRepository genresRepository)
        {
            if (!await moviesRepository.Exists(id))
            {
                return TypedResults.NotFound();
            }

            var existingGenres = new List<int>();

            if (genreIds.Count != 0)
            {
                existingGenres = await genresRepository.Exists(genreIds);
            }
            if (existingGenres.Count != genreIds.Count)
            {
                var nonExistingGenres = genreIds.Except(existingGenres);
                return TypedResults.BadRequest("One or more genre IDs are invalid.");
            }

            await moviesRepository.Assign(id, genreIds);
            return TypedResults.NoContent();
        }

        static async Task<Results<NoContent, NotFound, BadRequest<string>>> AssignActors(
            int id,
            List<AssignActorMovieDTO> actorsDTO,
            IMoviesRepository moviesRepository,
            IActorsRepository actorsRepository,
            IMapper mapper)
        {
            if (!await moviesRepository.Exists(id))
            {
                return TypedResults.NotFound();
            }

            var existingActors = new List<int>();
            var actorsIds = actorsDTO.Select(a => a.ActorId).ToList();

            if (actorsDTO.Count != 0)
            {
                existingActors = await actorsRepository.Exists(actorsIds);
            }
            if (existingActors.Count != actorsDTO.Count)
            {
                var nonExistingActors = actorsIds.Except(existingActors);
                var nonExistingActorsCSV = string.Join(",", nonExistingActors);
                return TypedResults.BadRequest($"One or more actor IDs are invalid: {nonExistingActorsCSV}");
            }

            var actors = mapper.Map<List<ActorMovie>>(actorsDTO);
            await moviesRepository.Assign(id, actors);
            return TypedResults.NoContent();
        }
    }
}
