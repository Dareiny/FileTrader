using FileTrader.AppServices.Auth.Services;
using FileTrader.Contracts.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FileTrader.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TokenController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public TokenController(IConfiguration configuration, ITokenService tokenService)
        {
            _configuration = configuration;
            _tokenService = tokenService;
        }
        /// <summary>
        /// Создаёт токен аутентификации.
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateUserRequest userData)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var token = await _tokenService.GenerateToken(userData, key);
            return Ok(new TokenDTO
            {
                TokenId = new JwtSecurityTokenHandler().WriteToken(token) 
            });
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromHeader(Name = "Authorization")] string authorizationHeader)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            if (!authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Invalid authorization header format.");
            }

            // Извлекаем токен из заголовка
            string token = authorizationHeader.Substring("Bearer ".Length).Trim();


            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Токен не предоставлен");
            }

            var userName = await _tokenService.GetLoginFromToken(token, key);

            if (userName == null)
            {
                return BadRequest("Неверный токен или пользователь не найден");
            }

            
            return Ok(userName);
        }


    }
}
