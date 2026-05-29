using Microsoft.AspNetCore.Identity;

namespace Minimal_API_Movies.Repositories
{
    public interface IUsersRepository
    {
        Task<string> Create(IdentityUser user);
        Task<IdentityUser?> GetByEmail(string normalizedEmail);
    }
}