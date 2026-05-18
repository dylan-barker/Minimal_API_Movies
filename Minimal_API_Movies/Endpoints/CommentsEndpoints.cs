using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using Minimal_API_Movies.DTOs;
using Minimal_API_Movies.Entities;
using Minimal_API_Movies.Repositories;

namespace Minimal_API_Movies.Endpoints
{
    public static class CommentsEndpoints
    {
        public static RouteGroupBuilder MapComments(this RouteGroupBuilder group)
        {
            group.MapPost("/", Create);
            return group;
        }

    static async Task<Results<Created<CommentDTO>, NotFound>> Create(int movieId,
        CreateCommentDTO createCommentDTO, ICommentsRepository commentsRepository, IMoviesRepository moviesRepository,
        IMapper mapper, IOutputCacheStore outputCacheStore)
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
            return TypedResults.Created($"/comment/{id}", commentDTO);
        }
    }
}
