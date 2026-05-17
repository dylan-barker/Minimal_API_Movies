using Minimal_API_Movies.Entities;

namespace Minimal_API_Movies.Repositories
{
    public interface ICommentsRepository
    {
        Task<int> Create(Comment comment);
        Task Delete(int id);
        Task<bool> Exists(int id);
        Task<List<Comment>> GetAll(int movieId);
        Task<Comment?> GetById(int id);
        Task Update(Comment comment);
    }
}