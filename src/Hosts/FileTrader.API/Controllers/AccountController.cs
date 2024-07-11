using FileTrader.AppServices.Auth.Services;
using FileTrader.AppServices.Users.Services;
using FileTrader.Contracts.Accounts;
using FileTrader.Contracts.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace FileTrader.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с аккаунтом.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        public AccountController(IConfiguration configuration, ITokenService tokenService, IUserService userService)
        {
            _configuration = configuration;
            _tokenService = tokenService;
            _userService = userService;
        }


        /// <summary>
        /// Создать сессию.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Статус операции.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAccount([FromQuery] CreateUserRequest request, CancellationToken cancellationToken)
        {
            // Создаём аккаунт
            var userId = await _userService.AddAsync(request, cancellationToken);


            // Получаем токен
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var token = await _tokenService.GenerateToken(request, key);
            var TokenOut = new TokenDTO
            {
                TokenId = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return CreatedAtAction(nameof(RegisterAccount),
                new { userId, TokenOut.TokenId }
                );
        }

        /// <summary>
        /// Вход в сессию.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Статус операции.</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromQuery] AccountLoginRequest request, CancellationToken cancellationToken)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var userbyNameRequest = new UsersByNameRequest { Login = request.Login };
            var userCollection = await _userService.GetUserByNameAsync(userbyNameRequest, cancellationToken);
            var user = userCollection.Result.FirstOrDefault();

            if (request.Password != user.Password) {
                return BadRequest("Неверно введён логин или пароль.");
            }

            var token = await _tokenService.GenerateToken(user, key);

            return Ok(token);
        }
    }
}
