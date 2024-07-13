using FileTrader.AppServices.Auth.Services;
using FileTrader.AppServices.Users.Services;
using FileTrader.Contracts.General;
using FileTrader.Contracts.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;

namespace FileTrader.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователем.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Инициализируем экземпляр <see cref="UserController"/>.
        /// </summary>
        /// <param name="userService">Сервис работы с пользователем.</param>
        public UserController(
            IUserService userService,
            IConfiguration configuration,
            ITokenService tokenService,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _userService = userService;
            _configuration = configuration;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
        }


        /// <summary>
        /// Возвращает список всех пользователей.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список всех пользователей по страницам <see cref="OkObjectResult"/>.</returns>
        [HttpGet("all")]
        [ProducesResponseType(typeof(ResultWithPagination<UserDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllUsers([FromQuery] PaginationRequest request, CancellationToken cancellationToken)
        {
            var result = await _userService.GetUsersAsync(request, cancellationToken);

            return Ok(result);
        }


        /// <summary>
        /// Обновляет данные пользователя
        /// </summary>
        /// <param name="request">Запрос обновления.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns><see cref="OkObjectResult"/>.</returns>
        [HttpPut("id")]
        [ProducesResponseType(typeof(IEnumerable<UserDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromQuery] UpdateUserRequest request, CancellationToken cancellationToken)
        {

            var id = await GetAuthorizedIdAsync();

            if (id == Guid.Empty || request == null)
            {
                return BadRequest("Invalid user data.");
            }

            var userToUpdate = await _userService.GetByIdAsync(id, cancellationToken);
            if (userToUpdate == null)
            {
                return NotFound("User not found.");
            }

            //Проверки наличия поля, если нет, то в БД не меняем на Null
            if (!string.IsNullOrEmpty(request.Login))
            {
                userToUpdate.Login = request.Login;
            }

            if (!string.IsNullOrEmpty(request.UserEmail))
            {
                userToUpdate.UserEmail = request.UserEmail;
            }
            if (!string.IsNullOrEmpty(request.Password))
            {
                userToUpdate.Password = request.Password;
            }




            await _userService.UpdateAsync(userToUpdate, cancellationToken);

            return NoContent();
        }



        /// <summary>
        /// Удаляет пользователя.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns><see cref="OkObjectResult"/>.</returns>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Authorize]
        public async Task<IActionResult> DeleteUser(CancellationToken cancellationToken)
        {
            var id = await GetAuthorizedIdAsync();

            var user = await _userService.GetByIdAsync(id, cancellationToken);
            if (user == null) return NotFound();

            await _userService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }

        private async Task<Guid> GetAuthorizedIdAsync()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            // Проверка заголовка
            if (!authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException("Некорректный формат заголовка авторизации.");
            }

            // Извлечение токена из заголовка
            string token = authorizationHeader.Substring("Bearer ".Length).Trim();

            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Токен не предоставлен");
            }

            var userId = await _tokenService.GetIdFromToken(token, key);

            if (userId == null)
            {
                throw new UnauthorizedAccessException("Неверный токен или пользователь не найден");
            }

            return Guid.Parse(userId);
        }

        private async Task<string> GetAuthorizedLoginAsync()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            // Проверка заголовка
            if (!authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException("Некорректный формат заголовка.");
            }

            // Извлечение токена из заголовка
            string token = authorizationHeader.Substring("Bearer ".Length).Trim();

            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Токен не предоставлен");
            }

            var userLogin = await _tokenService.GetLoginFromToken(token, key);

            if (userLogin == null)
            {
                throw new UnauthorizedAccessException("Неверный токен или пользователь не найден");
            }

            return userLogin;
        }
    }
}
