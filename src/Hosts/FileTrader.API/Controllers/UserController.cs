using FileTrader.AppServices.Users.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileTrader.API.Controllers
{

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
        /// Получить список всех пользователей.
        /// </summary>
        /// <param name="cancellationToken">токен отмены операции.</param>
        /// <returns>список всех пользователей <see cref="OkObjectResult"/>.</returns>
        [HttpGet]
        [Route ("all")]
        public async Task<IActionResult> GetAllUsers (CancellationToken cancellationToken)
        {
            var result = await _userService.GetUsersAsync (cancellationToken);
            return Ok(result);
        }
    }
}
