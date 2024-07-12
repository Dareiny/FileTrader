using FileTrader.AppServices.Auth.Services;
using FileTrader.AppServices.Users.Services;
using FileTrader.Contracts.Accounts;
using FileTrader.Contracts.Users;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
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

        /// <summary>
        /// Инициализация экземпляра
        /// </summary>
        public AccountController(IConfiguration configuration, ITokenService tokenService, IUserService userService)
        {
            _configuration = configuration;
            _tokenService = tokenService;
            _userService = userService;
        }


        /// <summary>
        /// Создать сессию.
        /// </summary>
        /// <param name="request">Запрос регистрации.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Статус операции <see cref="OkObjectResult"/>.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAccount([FromQuery] CreateUserRequest request, CancellationToken cancellationToken)
        {
            // Создаём аккаунт
            var userId = await _userService.AddAsync(request, cancellationToken);
            var user = await _userService.GetByIdAsync(userId, cancellationToken);

            // Получаем токен
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);


            var token = await _tokenService.GenerateToken(user, key);
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
        /// <param name="request">Запрос авторизации.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Статус операции <see cref="OkObjectResult"/>.</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromQuery] AccountLoginRequest request, CancellationToken cancellationToken)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var userbyNameRequest = new UsersByNameRequest { Login = request.Login };
            var user = await _userService.GetUserByNameAsync(userbyNameRequest, cancellationToken);
            if (user == null)
            {
                return BadRequest("Неверно введён логин или пароль.");
            }

            if (request.Password != user.Password)
            {
                return BadRequest("Неверно введён логин или пароль.");
            }

            var token = await _tokenService.GenerateToken(user, key);
            var TokenOut = new TokenDTO
            {
                TokenId = new JwtSecurityTokenHandler().WriteToken(token)
            };
            return Ok(TokenOut);
        }
    }
}
