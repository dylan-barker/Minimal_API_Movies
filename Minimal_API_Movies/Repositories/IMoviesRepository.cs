using Minimal_API_Movies.DTOs;
using Minimal_API_Movies.Entities;

namespace Minimal_API_Movies.Repositories
{
    public interface IMoviesRepository
    {
        Task<int> Create(Movie movie);
        Task Delete(int id);
        Task<bool> Exists(int id);
        Task<List<Movie>> GetAll(PaginationDTO pagination);
        Task<Movie?> GetById(int id);
        Task Update(Movie movie);
    }
}