using FileTrader.AppServices.Specifications;
using FileTrader.Contracts.General;
using FileTrader.Contracts.UserFiles;
using FileTrader.Domain.Files.Entity;
namespace FileTrader.AppServices.UserFiles.Repositories
{
    /// <summary>
    /// Репозиторий для работы с файлами.
    /// </summary>
    public interface IUserFilesRepository
    {

        /// <summary>
        /// Получение информации о файле.
        /// </summary>
        /// <param name="Id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Информация о файле.</returns>
        Task<FileInfoDTO> GetInfoByIdAsync(Guid Id, CancellationToken cancellationToken);

        /// <summary>
        /// Получение всех файлов по спецификации.
        /// </summary>
        /// <param name="request">Запрос на создание страниц.</param>
        /// <param name="specification">Спецификация.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Страница файлов.</returns>
        Task<ResultWithPagination<FileInfoDTO>> GetAllBySpecification(PaginationRequest request, Specification<EFile> specification, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет файл.
        /// </summary>
        /// <param name="Id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns></returns>
        Task DeleteByIdAsync(Guid Id, CancellationToken cancellationToken);

        /// <summary>
        /// Изменяет доступ к файлу.
        /// </summary>
        /// <param name="request">Запрос обновления</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns></returns>
        Task UpdateAccessAsync(UpdateAccessRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Загружает файл в систему.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Идентификатор загруженного файла.</returns>
        Task<Guid> UploadAsync(EFile file, CancellationToken cancellationToken);

        /// <summary>
        /// Скачивание файла
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Файл <see cref="FileDTO"/>.</returns>
        Task<FileDTO> DownloadAsync(Guid id, CancellationToken cancellationToken);
    }
}
