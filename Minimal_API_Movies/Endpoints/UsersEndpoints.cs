using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Minimal_API_Movies.DTOs;
using Minimal_API_Movies.Filters;
using Minimal_API_Movies.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Minimal_API_Movies.Endpoints
{
    public static class UsersEndpoints
    {
        public static RouteGroupBuilder MapUsersEndpoints(this RouteGroupBuilder group)
        {
            group.MapPost("/register", Register)
                .AddEndpointFilter<ValidationFilter<UserCredentialsDTO>>();
            group.MapPost("/login", Login)
                .AddEndpointFilter<ValidationFilter<UserCredentialsDTO>>();
            return group;
        }

        static async Task<Results<Ok<AuthenticationResponseDTO>,
            BadRequest<IEnumerable<IdentityError>>>> Register(UserCredentialsDTO userCredentialsDTO,
            [FromServices] UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            var user = new IdentityUser
            {
                UserName = userCredentialsDTO.Email,
                Email = userCredentialsDTO.Email
            };

            var result = userManager.CreateAsync(user, userCredentialsDTO.Password).Result;

            if (result.Succeeded)
            {
                var authenticationResponse = await
                    BuildToken(
                        userCredentialsDTO,
                        configuration,
                        userManager);
                return TypedResults.Ok(authenticationResponse);
            }
            else
            {
                return TypedResults.BadRequest(result.Errors);
            }
        }

        static async Task<Results<Ok<AuthenticationResponseDTO>, BadRequest<string>>> Login(
            UserCredentialsDTO userCredentialsDTO,
            [FromServices] SignInManager<IdentityUser> signInManager,
            [FromServices] UserManager<IdentityUser> userManager,
            IConfiguration configuration)
        {
            var user = await userManager.FindByEmailAsync(userCredentialsDTO.Email);

            if (user is null)
            {
                return TypedResults.BadRequest("Invalid email or password");
            }

            var result = await signInManager.CheckPasswordSignInAsync(
                user, 
                userCredentialsDTO.Password, 
                lockoutOnFailure: false);

            if (result.Succeeded) 
            {
                var authenticationResponse = await
                    BuildToken(
                        userCredentialsDTO,
                        configuration,
                        userManager);
                return TypedResults.Ok(authenticationResponse);
            }
            else
            {
                return TypedResults.BadRequest("Invalid email or password");
            }
        }

        private async static Task<AuthenticationResponseDTO> BuildToken(
            UserCredentialsDTO userCredentialsDTO,
            IConfiguration configuration,
            UserManager<IdentityUser> userManager)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", userCredentialsDTO.Email),
            };

            var key = KeysHandler.GetKey(configuration, KeysHandler.OurIssuer).First();
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMonths(1);

            var securityToken = new JwtSecurityToken(
                issuer: KeysHandler.OurIssuer,
                audience: KeysHandler.OurIssuer,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return new AuthenticationResponseDTO
            {
                Token = token,
                Expiration = expiration
            };
        }
    }
}
