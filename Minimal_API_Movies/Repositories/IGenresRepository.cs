using Minimal_API_Movies.Entities;

namespace Minimal_API_Movies.Repositories
{
    public interface IGenresRepository
    {
        Task<int> Create(Genre genre);
        Task<List<Genre>> GetAll();
        Task<Genre?> GetById(int id);
    }
}
