using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using Minimal_API_Movies.DTOs;
using Minimal_API_Movies.Entities;
using Minimal_API_Movies.Filters;
using Minimal_API_Movies.Repositories;

namespace Minimal_API_Movies.Endpoints
{
    public static class CommentsEndpoints
    {
        public static RouteGroupBuilder MapComments(this RouteGroupBuilder group)
        {
            group.MapPost("/", Create).AddEndpointFilter<ValidationFilter<CreateCommentDTO>>();
            group.MapGet("/", GetAll)
                .CacheOutput(c => c.Expire(TimeSpan.FromMinutes(1)).Tag("comments-get"));
            group.MapGet("/{commentId:int}", GetById).WithName("GetCommentById");
            group.MapPut("/{commentId:int}", Update).AddEndpointFilter<ValidationFilter<CreateCommentDTO>>();
            group.MapDelete("/{commentId:int}", Delete);
            return group;
        }

        static async Task<Results<Ok<List<CommentDTO>>, NotFound>> GetAll(
            int movieId,
            ICommentsRepository commentsRepository,
            IMoviesRepository moviesRepository,
            IMapper mapper,
            IOutputCacheStore outputCacheStore)
        {
            if (!await moviesRepository.Exists(movieId))
            {
                return TypedResults.NotFound();
            }

            var comments = await commentsRepository.GetAll(movieId);
            var commentsDTO = mapper.Map<List<CommentDTO>>(comments);
            return TypedResults.Ok(commentsDTO);
        }

        static async Task<Results<Ok<CommentDTO>, NotFound>> GetById(
            int movieId, int commentId,
            ICommentsRepository commentsRepository,
            IMoviesRepository moviesRepository,
            IMapper mapper,
            IOutputCacheStore outputCacheStore)
        {
            if (!await moviesRepository.Exists(movieId))
            {
                return TypedResults.NotFound();
            }

            var comment = await commentsRepository.GetById(commentId);
            if (comment is null)
            {
                return TypedResults.NotFound();
            }

            var commentDTO = mapper.Map<CommentDTO>(comment);
            return TypedResults.Ok(commentDTO);
        }
        static async Task<Results<CreatedAtRoute<CommentDTO>, NotFound>> Create(
            int movieId,
            CreateCommentDTO createCommentDTO,
            ICommentsRepository commentsRepository,
            IMoviesRepository moviesRepository,
            IMapper mapper,
            IOutputCacheStore outputCacheStore)
        {
            if (!await moviesRepository.Exists(movieId))
            {
                return TypedResults.NotFound();
            }

            var comment = mapper.Map<Comment>(createCommentDTO);
            comment.MovieId = movieId;
            var id = await commentsRepository.Create(comment);
            await outputCacheStore.EvictByTagAsync("comments-get", default);
            var commentDTO = mapper.Map<CommentDTO>(comment);
            return TypedResults.CreatedAtRoute(commentDTO, "GetCommentById", new { id, movieId });
        }

        static async Task<Results<NoContent, NotFound>> Update(
            int movieId, int commentId,
            CreateCommentDTO createCommentDTO,
            ICommentsRepository commentsRepository,
            IMoviesRepository moviesRepository,
            IMapper mapper,
            IOutputCacheStore outputCacheStore)
        {
            if (!await moviesRepository.Exists(movieId))
            {
                return TypedResults.NotFound();
            }
            if (!await commentsRepository.Exists(commentId))
            {
                return TypedResults.NotFound();
            }
            var comment = mapper.Map<Comment>(createCommentDTO);
            comment.Id = commentId;
            comment.MovieId = movieId;
            await commentsRepository.Update(comment);
            await outputCacheStore.EvictByTagAsync("comments-get", default);
            return TypedResults.NoContent();
        }

        static async Task<Results<NoContent, NotFound>> Delete(
            int movieId, int commentId,
            ICommentsRepository commentsRepository,
            IMoviesRepository moviesRepository,
            IMapper mapper,
            IOutputCacheStore outputCacheStore)
        {
            if (!await moviesRepository.Exists(movieId))
            {
                return TypedResults.NotFound();
            }
            if (!await commentsRepository.Exists(commentId))
            {
                return TypedResults.NotFound();
            }

            await commentsRepository.Delete(commentId);
            await outputCacheStore.EvictByTagAsync("comments-get", default);
            return TypedResults.NoContent();
        }
    }
}