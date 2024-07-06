using FileTrader.AppServices.Users.Services;
using FileTrader.Contracts.Users;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FileTrader.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователем
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        /// <summary>
        /// Инициализируем экземпляр <see cref="UserController"/>.
        /// </summary>
        /// <param name="userService">Сервис работы с пользователем.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        /// <summary>
        /// Возвращает список всех пользователей.
        /// </summary>
        /// <param name="cancellationToken">токен отмены операции.</param>
        /// <returns>список всех пользователей <see cref="OkObjectResult"/>.</returns>
        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<UserDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllUsers (CancellationToken cancellationToken)
        {
            var result = await _userService.GetUsersAsync (cancellationToken);
            return Ok(result);
        }
    }
}
