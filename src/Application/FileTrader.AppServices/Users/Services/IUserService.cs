using FileTrader.Contracts.General;
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
    /// Сервис работы с пользователями.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Возвращает всех пользователей.
        /// </summary>
        /// <returns>Список пользователей <see cref="UserDTO"/>.</returns>
        Task<ResultWithPagination<UserDTO>> GetUsersAsync(PaginationRequest request,CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает пользователей по имени.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список пользователей <see cref="UserDTO"/>Коллекция моделей пользователей.</returns>
        Task<ResultWithPagination<UserDTO>> GetUserByNameAsync(UsersByNameRequest request2, CancellationToken cancellationToken);


        /// <summary>
        /// Возвращает все элементы сущности "пользователи" по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns><see cref="UserDTO"/>.</returns>
        ValueTask<UserDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет записи.
        /// </summary>
        /// <param name="entity">Записи.</param>
        /// <returns></returns>
        Task<Guid> AddAsync(CreateUserRequest entity, CancellationToken cancellationToken);

        /// <summary>
        /// Обновляет записи.
        /// </summary>
        /// <param name="entity">Записи.</param>
        /// <returns></returns>
        Task UpdateAsync(UserDTO entity, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет записи по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }

}
