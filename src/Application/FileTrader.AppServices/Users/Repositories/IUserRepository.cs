using FileTrader.Contracts.Users;
using FileTrader.Domain.Users.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FileTrader.AppServices.Users.Repositories
{
    /// <summary>
    /// Репозиторий для работы с пользователем.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Возвращает всех пользователей.
        /// </summary>
        /// <param name="cancellationToken">токен отмены операции.</param>
        /// <returns>Список пользователей.</returns>
        Task<IEnumerable<UserDTO>> GetAllAsync(CancellationToken cancellationToken);

        Task<IEnumerable<UserDTO>> GetFiltered(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken);

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
        Task AddAsync(UserDTO entity, CancellationToken cancellationToken);

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
