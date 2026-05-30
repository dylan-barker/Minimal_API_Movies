using Microsoft.AspNetCore.Identity;
using Minimal_API_Movies.Repositories;
using System.Security.Claims;

namespace Minimal_API_Movies.Services
{
    public class UserStore : IUserStore<IdentityUser>, IUserEmailStore<IdentityUser>,
        IUserPasswordStore<IdentityUser>, IUserClaimStore<IdentityUser>
    {
        private readonly IUsersRepository usersRepository;

        public UserStore(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }
        public async Task<IdentityResult> CreateAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            user.Id = await usersRepository.Create(user);
            return IdentityResult.Success;
        }

        public async Task<IdentityUser?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return await usersRepository.GetByEmail(normalizedEmail);
        }

        public async Task<IdentityUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await usersRepository.GetByEmail(normalizedUserName);
        }

        public async Task<string?> GetEmailAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Email);
        }

        public async Task<string?> GetPasswordHashAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.PasswordHash);
        }

        public async Task<string> GetUserIdAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Id.ToString());
        }

        public async Task<string?> GetUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Email);
        }

        public Task SetNormalizedEmailAsync(IdentityUser user, string? normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(IdentityUser user, string? normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(IdentityUser user, string? passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public async Task AddClaimsAsync(IdentityUser user,
            IEnumerable<Claim> claims,
            CancellationToken cancellationToken)
        {
            await usersRepository.AssignClaims(user, claims);
        }

        public Task<IdentityResult> UpdateAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public async Task RemoveClaimsAsync(IdentityUser user,
            IEnumerable<Claim> claims,
            CancellationToken cancellationToken)
        {
            await usersRepository.RemoveClaims(user, claims);
        }

        public async Task<IList<Claim>> GetClaimsAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return await usersRepository.GetClaims(user);
        }

        public Task<IdentityResult> DeleteAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser?> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetEmailConfirmedAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetNormalizedEmailAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetNormalizedUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(IdentityUser user, string? email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(IdentityUser user, string? userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceClaimAsync(IdentityUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<IdentityUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
