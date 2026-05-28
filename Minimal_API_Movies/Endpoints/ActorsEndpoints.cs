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
    public static class ActorsEndpoints
    {
        private readonly static string container = "actors"; // For file storage
        public static RouteGroupBuilder MapActors(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAll)
                .CacheOutput(c => c.Expire(TimeSpan.FromMinutes(2)).Tag("actors-get"));
            group.MapGet("/{id:int}", GetById);
            group.MapGet("/getByName/{name}", GetByName);
            group.MapPost("/", Create).DisableAntiforgery().AddEndpointFilter<ValidationFilter<CreateActorDTO>>();
            group.MapPut("/{id:int}", Update).DisableAntiforgery().AddEndpointFilter<ValidationFilter<CreateActorDTO>>();
            group.MapDelete("/{id:int}", Delete).DisableAntiforgery();
            return group;
        }

        static async Task<Ok<List<ActorDTO>>> GetAll(IActorsRepository repository, IMapper mapper,
            int page = 1, int recordsPerPage = 10)
        {
            var pagination = new PaginationDTO { Page = page, RecordsPerPage = recordsPerPage };
            var actors = await repository.GetAll(pagination);
            var actorsDTO = mapper.Map<List<ActorDTO>>(actors);
            return TypedResults.Ok(actorsDTO);
        }

        static async Task<Results<Ok<ActorDTO>, NotFound>> GetById(int id, IActorsRepository repository, IMapper mapper)
        {
            var actor = await repository.GetById(id);
            if (actor is null)
            {
                return TypedResults.NotFound();
            }
            var actorDTO = mapper.Map<ActorDTO>(actor);
            return TypedResults.Ok(actorDTO);
        }

        static async Task<Created<ActorDTO>> Create(
            [FromForm] CreateActorDTO createActorDTO,
            IActorsRepository repository,
            IOutputCacheStore cacheStore,
            IMapper mapper,
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

        static async Task<Ok<List<ActorDTO>>> GetByName(string name, IActorsRepository repository, IMapper mapper)
        {
            var actors = await repository.GetByName(name);
            var actorsDTO = mapper.Map<List<ActorDTO>>(actors);
            return TypedResults.Ok(actorsDTO);
        }

        static async Task<Results<NoContent, NotFound>> Update(int id, [FromForm] CreateActorDTO createActorDTO,
            IActorsRepository repository, IMapper mapper, IFileStorage fileStorage, IOutputCacheStore outputCacheStore)
        {
            var actor = await repository.GetById(id);
            if (actor is null)
            {
                return TypedResults.NotFound();
            }

            var actorForUpdate = mapper.Map<Actor>(createActorDTO);
            actorForUpdate.Id = id;
            actorForUpdate.Picture = actor.Picture;

            if (createActorDTO.Picture is not null)
            {
                var url = await fileStorage.Edit(actorForUpdate.Picture, container, createActorDTO.Picture);
                actorForUpdate.Picture = url;
            }

            await repository.Update(actorForUpdate);
            await outputCacheStore.EvictByTagAsync("actors-get", default);
            return TypedResults.NoContent();
        }

        static async Task<Results<NoContent, NotFound>> Delete(int id, IActorsRepository repository,
            IOutputCacheStore outputCacheStore, IFileStorage fileStorage)
        {
            var actor = await repository.GetById(id);
            if (actor is null)
            {
                return TypedResults.NotFound();
            }

            await repository.Delete(id);
            await fileStorage.Delete(actor.Picture, container);
            await outputCacheStore.EvictByTagAsync("actors-get", default);
            return TypedResults.NoContent();
        }
    }
}
