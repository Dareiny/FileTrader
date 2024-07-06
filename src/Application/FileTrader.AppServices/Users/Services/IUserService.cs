using FileTrader.Contracts.Users;
using System;
using System.Collections.Generic;
using System.Linq;
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
        Task<IEnumerable<UserDTO>> GetUsersAsync(CancellationToken cancellationToken);
    }
}
