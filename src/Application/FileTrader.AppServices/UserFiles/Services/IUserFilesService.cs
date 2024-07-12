using FileTrader.AppServices.Specifications;
using FileTrader.Contracts.General;
using FileTrader.Contracts.UserFiles;
using FileTrader.Domain.Files.Entity;

namespace FileTrader.AppServices.UserFiles.Services
{
    /// <summary>
    /// Сервис работы с файлами.
    /// </summary>
    public interface IUserFilesService
    {
        /// <summary>
        /// Получение информации о файле по идентификатору.
        /// </summary>
        /// <param name="Id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Информация о файле <see cref="FileInfoDTO"/>.</returns>
        Task<FileInfoDTO> GetInfoByIdAsync(Guid Id, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает все доступные файлы.
        /// </summary>
        /// <param name="request">Запрос на создание страниц.</param>
        /// <param name="specification">Спецификация.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список файлов <see cref="FileInfoDTO"/>.</returns>
        public Task<ResultWithPagination<FileInfoDTO>> GetFilesAsync(PaginationRequest request, Specification<EFile> specification, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет файл.
        /// </summary>
        /// <param name="Id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns></returns>
        Task DeleteByIdAsync(Guid Id, CancellationToken cancellationToken);

        /// <summary>
        /// Обновляет доступ к файлу.
        /// </summary>
        /// <param name="request">Запрос обновления</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns></returns>
        Task UpdateAccessAsync(UpdateAccessRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Загрузка файла в систему.
        /// </summary>
        /// <param name="model">Модель файла.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Идентификатор загруженного файла.</returns>
        Task<Guid> UploadAsync(FileDTO model, CancellationToken cancellationToken);

        /// <summary>
        /// Скачивание файла
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Файл <see cref="FileDTO"/>.</returns>
        Task<FileDTO> DownloadAsync(Guid id, CancellationToken cancellationToken);
    }

}
