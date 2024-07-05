using FileTrader.Contracts.Users;
using System;
using System.Collections.Generic;
using System.Linq;
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
        Task<IEnumerable<UserDTO>> GetAll(CancellationToken cancellationToken);
    }
}
