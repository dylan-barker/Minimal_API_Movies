using Minimal_API_Movies.Entities;

namespace Minimal_API_Movies.Repositories
{
    public interface IErrorsRepository
    {
        Task<Guid> Create(Error error);
    }
}