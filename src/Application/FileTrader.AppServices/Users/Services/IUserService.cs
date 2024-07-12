using FileTrader.Contracts.General;
using FileTrader.Contracts.Users;

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
        /// <param name="request">Запрос на создание страниц.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список пользователей <see cref="UserInfoDTO"/>.</returns>
        Task<ResultWithPagination<UserInfoDTO>> GetUsersAsync(PaginationRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает пользователя по имени.
        /// </summary>
        /// <param name="request2">Запрос по имени.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns><see cref="UserDTO"/>.</returns>
        Task<UserDTO> GetUserByNameAsync(UsersByNameRequest request2, CancellationToken cancellationToken);


        /// <summary>
        /// Возвращает пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns><see cref="UserDTO"/>.</returns>
        ValueTask<UserDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет запись.
        /// </summary>
        /// <param name="entity">Запись.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns><see cref="Guid"/>.</returns>
        Task<Guid> AddAsync(CreateUserRequest entity, CancellationToken cancellationToken);

        /// <summary>
        /// Обновляет записи.
        /// </summary>
        /// <param name="entity">Записи.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns></returns>
        Task UpdateAsync(UserDTO entity, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет запись по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }

}
