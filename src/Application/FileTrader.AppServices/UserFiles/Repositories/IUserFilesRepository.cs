using FileTrader.AppServices.Specifications;
using FileTrader.Contracts.UserFiles;
using FileTrader.Domain.Files.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
        /// Удаляет файл.
        /// </summary>
        /// <param name="Id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns></returns>
        Task DeleteByIdAsync(Guid Id, CancellationToken cancellationToken);

        /// <summary>
        /// Загружает файл в систему.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Идентификатор загруженного файла.</returns>
        Task<Guid> UploadAsync(UserFile file, CancellationToken cancellationToken);

        /// <summary>
        /// Скачивание файла
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Файл <see cref="FileDTO"/>.</returns>
        Task<FileDTO> DownloadAsync(Guid id, CancellationToken cancellationToken);
    }
}
