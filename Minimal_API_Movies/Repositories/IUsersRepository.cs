using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Minimal_API_Movies.Repositories
{
    public interface IUsersRepository
    {
        Task AssignClaims(IdentityUser user, IEnumerable<Claim> claims);
        Task<string> Create(IdentityUser user);
        Task<IdentityUser?> GetByEmail(string normalizedEmail);
        Task<IList<Claim>> GetClaims(IdentityUser user);
        Task RemoveClaims(IdentityUser user, IEnumerable<Claim> claims);
    }
}