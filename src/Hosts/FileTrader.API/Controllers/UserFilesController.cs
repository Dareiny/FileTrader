using FileTrader.AppServices.UserFiles.Services;
using FileTrader.Contracts.UserFiles;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        /// <summary>
        /// Инициализируем экземпляр <see cref="UserFilesController"/>.
        /// </summary>
        /// <param name="userFilesService">Сервис работы с файлом.</param>
        public UserFilesController(IUserFilesService userFilesService)
        {
            _userFilesService = userFilesService;
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
            var result = await _userFilesService.DownloadAsync(id, cancellationToken);
            if (result == null) StatusCode((int)HttpStatusCode.NotFound);

            Response.ContentLength = result.Content.Length;
            return File(result.Content, result.ContentType);
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
        public async Task<IActionResult> Upload(IFormFile file, CancellationToken cancellationToken)
        {
            var bytes = await GetBytesAsync(file, cancellationToken);
            var fileDto = new FileDTO
            {
                Name = file.FileName,
                Content = bytes,
                ContentType = file.ContentType,
            };
            var result = await _userFilesService.UploadAsync(fileDto, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
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

        private async Task<byte[]> GetBytesAsync(IFormFile file, CancellationToken cancellationToken)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms, cancellationToken);
            return ms.ToArray();

        }

    }
}
