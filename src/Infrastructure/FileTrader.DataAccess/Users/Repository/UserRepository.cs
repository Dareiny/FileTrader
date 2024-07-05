using FileTrader.AppServices.Users.Repositories;
using FileTrader.Contracts.Users;
using FileTrader.Domain.Users.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrader.DataAccess.Users.Repository
{
    /// <inheritdoc />
    public class UserRepository : IUserRepository
    {
        /// <inheritdoc />
        public Task<IEnumerable<UserDTO>> GetAll(CancellationToken cancellationToken)
        {
            var users = UserList();

            return Task.Run(function:() => users.Select(u => new UserDTO
            {
                Id = u.Id,
                UserName = u.UserName,
                UserEmail = u.UserEmail,
            }), cancellationToken);
        }

        /// <summary>
        /// Заглушка для списка пользователей.
        /// </summary>
        /// <returns></returns>
        public static List<User> UserList()
        {
            return null;
        }
    }
}
