using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Minimal_API_Movies.DTOs;
using Minimal_API_Movies.Entities;
using Minimal_API_Movies.Repositories;
using Minimal_API_Movies.Services;

namespace Minimal_API_Movies.Endpoints
{
    public static class ActorsEndpoints
    {
        private readonly static string container = "actors"; // For file storage
        public static RouteGroupBuilder MapActors(this RouteGroupBuilder group)
        {
            group.MapPost("/", Create).DisableAntiforgery();
            return group;
        }

        static async Task<Created<ActorDTO>> Create([FromForm] CreateActorDTO createActorDTO,
            IActorsRepository repository, IOutputCacheStore cacheStore, IMapper mapper,
            IFileStorage fileStorage)
        { 
            var actor = mapper.Map<Actor>(createActorDTO);
            if (createActorDTO.Picture != null)
            {
                var url = await fileStorage.Store(container, createActorDTO.Picture);
                actor.Picture = url;
            }
            var id = await repository.Create(actor);
            await cacheStore.EvictByTagAsync("actors-get", default);
            var actorDTO = mapper.Map<ActorDTO>(actor);
            return TypedResults.Created($"/actors/{id}", actorDTO);
        }
    }
}
