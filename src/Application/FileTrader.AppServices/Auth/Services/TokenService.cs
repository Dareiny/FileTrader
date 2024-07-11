using FileTrader.Contracts.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;

namespace FileTrader.AppServices.Auth.Services
{
    public class TokenService : ITokenService
    {
        public Task<JwtSecurityToken> GenerateToken(CreateUserRequest userData, byte[] key)
        {
            var claims = new[] {
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

        public Task<JwtSecurityToken> GenerateToken(UserDTO userData, byte[] key)
        {
            var claims = new[] {
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
            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();
            validationParameters.IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key);
            validationParameters.ValidateLifetime = true;
            validationParameters.ValidateAudience = false;
            validationParameters.ValidateIssuer = false;

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out validatedToken);

            var userName = principal.FindFirst(c => c.Type == ClaimTypes.Name)?.Value;

            return Task.FromResult(userName);
        }
    }
}
