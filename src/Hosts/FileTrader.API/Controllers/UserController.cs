using FileTrader.AppServices.Users.Services;
using FileTrader.Contracts.Users;
using Microsoft.AspNetCore.Http.HttpResults;
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
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список всех пользователей <see cref="OkObjectResult"/>.</returns>
        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<UserDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllUsers (CancellationToken cancellationToken)
        {
            var result = await _userService.GetUsersAsync (cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Возвращает запись пользователя по Id.
        /// </summary>
        /// <param name="id">Идентификатор <see cref="Guid"/>.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Запись пользователя <see cref="OkObjectResult"/>.</returns>
        [HttpGet("user")]
        [ProducesResponseType(typeof(IEnumerable<UserDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _userService.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Добавляет нового пользователя.
        /// </summary>
        /// <param name="request">Запрос контракта на создание записи пользователя.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Код создания <see cref="CreatedAtActionResult"/>.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(UserDTO),(int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateUser(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var dto = new UserDTO()
            {
                UserName = request.UserName,
                UserEmail = request.UserEmail
            };

            var result = await _userService.AddAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(CreateUser), new { result });
        }

        /// <summary>
        /// Обновить информацию о пользователе.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="request">Данные для обновления пользователя.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат обновления.</returns>
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(typeof(IEnumerable<UserDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserRequest request, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty || request == null)
            {
                return BadRequest("Invalid user data.");
            }

            var userToUpdate = await _userService.GetByIdAsync(id, cancellationToken);
            if (userToUpdate == null)
            {
                return NotFound("User not found.");
            }

            if (!string.IsNullOrEmpty(request.UserName))
            {
                userToUpdate.UserName = request.UserName;
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
        /// Удаляет запись пользователя по Id
        /// </summary>
        /// <param name="id">Идентификатор <see cref="Guid"/>.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат операции <see cref="OkObjectResult"/>.</returns>
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(typeof(IEnumerable<UserDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
        {
            await _userService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
