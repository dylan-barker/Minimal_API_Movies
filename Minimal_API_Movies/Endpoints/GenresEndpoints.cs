using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using Minimal_API_Movies.DTOs;
using Minimal_API_Movies.Entities;
using Minimal_API_Movies.Repositories;

namespace Minimal_API_Movies.Endpoints
{
    public static class GenresEndpoints
    {

        public static RouteGroupBuilder MapGenres(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetGenres)
                .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(30)).Tag("genres-get")); // Cache the response for 30 seconds
            group.MapGet("/{id:int}", GetGenreById);
            group.MapPost("/", CreateGenre);
            group.MapPut("/{id:int}", UpdateGenre);
            group.MapDelete("/{id:int}", DeleteGenre);
            return group;
        }

        static async Task<Ok<List<GenreDTO>>> GetGenres(IGenresRepository genresRepository, IMapper mapper)
        {
            var genres = await genresRepository.GetAll();
            var genresDTO = mapper.Map<List<GenreDTO>>(genres);

            return TypedResults.Ok(genresDTO);
        }

        static async Task<Results<Ok<GenreDTO>, NotFound>> GetGenreById(int id, IGenresRepository genresRepository, IMapper mapper)
        {
            var genre = await genresRepository.GetById(id);
            if (genre is null)
            {
                return TypedResults.NotFound();
            }
            var genreDTO = mapper.Map<GenreDTO>(genre);
            return TypedResults.Ok(genreDTO);
        }

        static async Task<Results<Created<GenreDTO>, BadRequest>> CreateGenre(CreateGenreDTO createGenreDTO, IGenresRepository genresRepository,
            IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var genre = mapper.Map<Genre>(createGenreDTO);
            await genresRepository.Create(genre);
            await outputCacheStore.EvictByTagAsync("genres-get", default);
            var genreDTO = mapper.Map<GenreDTO>(genre);
            return TypedResults.Created($"/genres/{genre.Id}", genreDTO);
        }

        static async Task<Results<NotFound, NoContent>> UpdateGenre(int id, CreateGenreDTO createGenreDTO, IGenresRepository genresRepository,
            IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var exists = await genresRepository.Exists(id);
            if (!exists)
            {
                return TypedResults.NotFound();
            }
            var genre = mapper.Map<Genre>(createGenreDTO);
            genre.Id = id;
            await genresRepository.Update(genre);
            await outputCacheStore.EvictByTagAsync("genres-get", default);
            return TypedResults.NoContent();
        }

        static async Task<Results<NotFound, NoContent>> DeleteGenre(int id, IGenresRepository genresRepository,
            IOutputCacheStore outputCacheStore)
        {
            var exists = await genresRepository.Exists(id);
            if (!exists)
            {
                return TypedResults.NotFound();
            }
            await genresRepository.Delete(id);
            await outputCacheStore.EvictByTagAsync("genres-get", default);
            return TypedResults.NoContent();
        }
    }
}
