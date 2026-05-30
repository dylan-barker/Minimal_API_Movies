using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Claims;

namespace Minimal_API_Movies.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly string connectionString;

        public UsersRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<IdentityUser?> GetByEmail(string normalizedEmail)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<IdentityUser>(
                    "Users_GetByEmail",
                    new { normalizedEmail },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<string> Create(IdentityUser user)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                user.Id = Guid.NewGuid().ToString();
                await connection.ExecuteAsync(
                    "Users_Create",
                    new
                    {
                        user.Id,
                        user.UserName,
                        user.NormalizedUserName,
                        user.Email,
                        user.NormalizedEmail,
                        user.PasswordHash
                    },
                    commandType: CommandType.StoredProcedure);
                return user.Id;
            }
        }

        public async Task<IList<Claim>> GetClaims(IdentityUser user)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var claims = await connection.QueryAsync<Claim>(
                    "Users_GetClaims",
                    new { user.Id },
                    commandType: CommandType.StoredProcedure);
                return claims.ToList();
            }
        }

        public async Task AssignClaims(IdentityUser user, IEnumerable<Claim> claims)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "Insert into UserClaims (UserId, ClaimType, ClaimValue) values (@UserId, @ClaimType, @ClaimValue)";
                var parameters = claims.Select(x => new
                {
                    user.Id,
                    x.Type,
                    x.Value
                });
                await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task RemoveClaims(IdentityUser user, IEnumerable<Claim> claims)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "Delete UserClaims where UserId = @Id and ClaimType = @Type";
                var parameters = claims.Select(x => new
                {
                    user.Id,
                    x.Type,
                });
                await connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}
