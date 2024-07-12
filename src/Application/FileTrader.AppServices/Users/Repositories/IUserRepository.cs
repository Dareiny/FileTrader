using FileTrader.AppServices.Specifications;
using FileTrader.Contracts.General;
using FileTrader.Contracts.Users;
using FileTrader.Domain.Users.Entity;

namespace FileTrader.AppServices.Users.Repositories
{
    /// <summary>
    /// Репозиторий для работы с пользователем.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Возвращает пользователей по заданной спецификацией логике.
        /// </summary>
        /// <param name="request">Запрос на создание страниц.</param>
        /// <param name="specification">Спецификация.</param>
        /// <param name="cancellation">Токен отмены операции.</param>
        /// <returns>Список всех пользователей <see cref="UserInfoDTO"/>.</returns>
        Task<ResultWithPagination<UserInfoDTO>> GetAllBySpecification(PaginationRequest request, Specification<User> specification, CancellationToken cancellation);

        /// <summary>
        /// Возвращает пользователя по идентификатору.
        /// </summary>
        /// <param name="specification">Спецификация.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns><see cref="UserDTO"/>.</returns>
        Task<UserDTO> GetByIdAsync(Specification<User> specification, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает пользователя по имени.
        /// </summary>
        /// <param name="specification">Спецификация.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns></returns>
        Task<UserDTO> GetByNameAsync(Specification<User> specification, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет пользователя.
        /// </summary>
        /// <param name="entity">Запись.</param>
        /// <returns><see cref="Guid"/>.</returns>
        Task AddAsync(User entity, CancellationToken cancellationToken);

        /// <summary>
        /// Обновляет запись пользователя.
        /// </summary>
        /// <param name="entity">Запись.</param>
        /// <returns></returns>
        Task UpdateAsync(UserDTO entity, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет пользователя.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
