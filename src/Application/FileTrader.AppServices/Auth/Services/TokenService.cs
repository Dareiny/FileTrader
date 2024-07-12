using FileTrader.Contracts.Users;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FileTrader.AppServices.Auth.Services
{
    /// <inheritdoc cref="ITokenService"/>
    public class TokenService : ITokenService
    {

        public Task<JwtSecurityToken> GenerateToken(UserDTO userData, byte[] key)
        {
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, userData.Id.ToString()),
                new Claim(ClaimTypes.Name, userData.Login),
                new Claim(ClaimTypes.Email, userData.UserEmail)
            };
            var TokenKey = new SymmetricSecurityKey(key);
            var signIn = new SigningCredentials(TokenKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                null,
                null,
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn
                );

            return Task.FromResult(token);
        }

        public Task<String> GetLoginFromToken(string token, byte[] key)
        {
            var principal = GetClaimsPrincipalFromToken(token, key);

            var userName = principal.FindFirst(c => c.Type == ClaimTypes.Name)?.Value;

            return Task.FromResult(userName);
        }

        public Task<String> GetIdFromToken(string token, byte[] key)
        {
            var principal = GetClaimsPrincipalFromToken(token, key);

            var userId = principal.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            return Task.FromResult(userId);
        }



        protected ClaimsPrincipal GetClaimsPrincipalFromToken(string token, byte[] key)
        {
            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();
            validationParameters.IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key);
            validationParameters.ValidateLifetime = true;
            validationParameters.ValidateAudience = false;
            validationParameters.ValidateIssuer = false;

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out validatedToken);

            return principal;
        }
    }
}
