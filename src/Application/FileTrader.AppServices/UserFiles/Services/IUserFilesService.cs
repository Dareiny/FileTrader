using FileTrader.Contracts.Users;
using FileTrader.Domain.Users.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FileTrader.AppServices.Users.Services
{
    /// <summary>
    /// Сервис работы с файлами.
    /// </summary>
    public interface IUserFilesService
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
        /// Загрузка файла в систему.
        /// </summary>
        /// <param name="model">Модель файла.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Идентификатор загруженного файла.</returns>
        Task<Guid> UploadAsync(FileDTO model, CancellationToken cancellationToken);
    }

}
