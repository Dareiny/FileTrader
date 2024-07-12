using FileTrader.AppServices.Auth.Services;
using FileTrader.AppServices.Specifications;
using FileTrader.AppServices.UserFiles.Services;
using FileTrader.AppServices.UserFiles.Specifications;
using FileTrader.Contracts.General;
using FileTrader.Contracts.UserFiles;
using FileTrader.Domain.Files.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;

namespace FileTrader.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с файлами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserFilesController : ControllerBase
    {
        private readonly IUserFilesService _userFilesService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;


        /// <summary>
        /// Инициализируем экземпляр <see cref="UserFilesController"/>.
        /// </summary>
        /// <param name="userFilesService">Сервис работы с файлом.</param>
        public UserFilesController(
            IUserFilesService userFilesService,
            ITokenService tokenService,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _userFilesService = userFilesService;
            _tokenService = tokenService;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Загружает файл из БД.
        /// </summary>
        /// <param name="id">Идентификатор <see cref="Guid"/>.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Файл.</returns>
        [HttpGet("get/{id}")]
        [ProducesResponseType(typeof(IEnumerable<FileDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Download(Guid id, CancellationToken cancellationToken)
        {

            //Проверка наличия файла
            var fileInfo = await _userFilesService.GetInfoByIdAsync(id, cancellationToken);
            if (fileInfo == null)
            {
                return BadRequest("Такого файла не существует.");
            }
            //Проверка доступа к файлу
            if (fileInfo.GeneralAccess)
            {
                var result1 = await _userFilesService.DownloadAsync(id, cancellationToken);

                if (result1 == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound);
                }

                Response.ContentLength = result1.Content.Length;
                return File(result1.Content, result1.ContentType, result1.Name);
            }

            var Uid = await GetAuthorizedIdAsync();

            if (Uid != fileInfo.OwnerId) throw new UnauthorizedAccessException("Только владелец имеет доступ к файлу.");

            var result = await _userFilesService.DownloadAsync(id, cancellationToken);

            if (result == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }

            Response.ContentLength = result.Content.Length;
            return File(result.Content, result.ContentType, result.Name);


        }



        /// <summary>
        /// Выгружает файл в БД.
        /// </summary>
        /// <param name="file">Файл с формы.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Код создания <see cref="CreatedAtActionResult"/>.</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize]
        public async Task<IActionResult> Upload(IFormFile file, CancellationToken cancellationToken)
        {
            var id = await GetAuthorizedIdAsync();

            var bytes = await GetBytesAsync(file, cancellationToken);
            var fileDto = new FileDTO
            {
                Name = file.FileName,
                Content = bytes,
                ContentType = file.ContentType,
                GeneralAccess = false,
                OwnerId = id,
            };
            var result = await _userFilesService.UploadAsync(fileDto, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }

        /// <summary>
        /// Обновляет уровень доступа к файлу.
        /// </summary>
        /// <param name="fileId">Идентификатор файла.</param>
        /// <param name="access">Доступен для скачивания всем.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Authorize]
        public async Task<IActionResult> GrantAccessToFile(Guid fileId, bool access, CancellationToken cancellationToken)
        {
            var id = await GetAuthorizedIdAsync();

            // Проверка, что текущий пользователь является владельцем файла
            var file = await _userFilesService.GetInfoByIdAsync(fileId, cancellationToken);
            if (file == null || file.OwnerId != id)
            {
                throw new UnauthorizedAccessException("Только владелец может менять уровень доступа.");
            }

            var req = new UpdateAccessRequest
            {
                GeneralAccess = access,
                Id = fileId,
            };
            await _userFilesService.UpdateAccessAsync(req, cancellationToken);

            return Ok();
        }


        /// <summary>
        /// Удаляет файл из БД.
        /// </summary>
        /// <param name="id">Идентификатор <see cref="Guid"/>.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат операции <see cref="OkObjectResult"/>.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _userFilesService.DeleteByIdAsync(id, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Получение информации о файле по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор <see cref="Guid"/>.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Информация о файле.</returns>
        [HttpGet("{id}/info")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetInfoById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _userFilesService.GetInfoByIdAsync(id, cancellationToken);
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Получения списка файлов доступных к скачиванию всем.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список файлов.</returns>
        [HttpGet("all-accessed/info")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAccessedFilesInfo([FromQuery] PaginationRequest request, CancellationToken cancellationToken)
        {
            //Спецификация для GeneralAccess
            var specification = new ByGeneralAccessSpecification(true);

            var result = await _userFilesService.GetFilesAsync(request, specification, cancellationToken);

            return Ok(result);
        }
        /// <summary>
        /// Получение списка файлов доступных к скачиванию.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список файлов.</returns>
        [HttpGet("all-accessed-for-user/info")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Authorize]
        public async Task<IActionResult> GetAccessedFilesInfoAuth([FromQuery] PaginationRequest request, CancellationToken cancellationToken)
        {
            //Спецификация для Id
            var id = await GetAuthorizedIdAsync();
            var ownerIdSpecification = new ByOwnerIdSpecification(id);

            //Спецификация для GeneralAccess
            var accessSpecification = new ByGeneralAccessSpecification(true);

            //Совмещение спецификаций
            var specification = new OrSpecification<EFile>(ownerIdSpecification, accessSpecification);

            var result = await _userFilesService.GetFilesAsync(request, specification, cancellationToken);

            return Ok(result);
        }

        private async Task<byte[]> GetBytesAsync(IFormFile file, CancellationToken cancellationToken)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms, cancellationToken);
            return ms.ToArray();

        }


        private async Task<Guid> GetAuthorizedIdAsync()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            // Проверка заголовка
            if (!authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException("Invalid authorization header format.");
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

    }
}
