using Minimal_API_Movies.Entities;

namespace Minimal_API_Movies.Repositories
{
    public interface IActorsRepository
    {
        Task<int> Create(Actor actor);
        Task<List<Actor>> GetAll();
        Task<Actor?> GetById(int id);
        Task<bool> Exist(int id);
        Task Update(Actor actor);
        Task Delete(int id);
    }
}
