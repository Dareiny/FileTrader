using FileTrader.Contracts.Users;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrader.AppServices.Auth.Services
{
    public interface ITokenService
    {
        public Task<JwtSecurityToken> GenerateToken(CreateUserRequest userData, byte[] key);
        public Task<JwtSecurityToken> GenerateToken(UserDTO userData, byte[] key);
        public Task<String> GetLoginFromToken(string token, byte[] key);
    }
    
}
