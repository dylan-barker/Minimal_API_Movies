using Microsoft.AspNetCore.Identity;

namespace Minimal_API_Movies.Services
{
    public interface IUsersService
    {
        Task<IdentityUser?> GetUser();
    }
}