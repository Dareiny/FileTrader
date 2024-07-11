using FileTrader.AppServices.Specifications;
using FileTrader.Contracts.General;
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
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список пользователей.</returns>
        //Task<ResultWithPagination<UserDTO>> GetAllAsync(GetAllUsersRequest request,CancellationToken cancellationToken);
        /// <summary>
        /// Возвращает пользователей по заданной спецификацией логике.
        /// </summary>
        /// <param name="specification">Спецификация.</param>
        /// <param name="cancellation">Токен отмены операции.</param>
        /// <returns></returns>
        Task<ResultWithPagination<UserDTO>> GetAllBySpecification(PaginationRequest request, Specification<User> specification, CancellationToken cancellation);

        /// <summary>
        /// Возвращает все элементы сущности "пользователи" по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns><see cref="UserDTO"/>.</returns>
        Task<UserDTO> GetByIdAsync(Specification<User> specification, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет записи.
        /// </summary>
        /// <param name="entity">Записи.</param>
        /// <returns></returns>
        Task AddAsync(User entity, CancellationToken cancellationToken);

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
