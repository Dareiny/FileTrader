using FileTrader.AppServices.Users.Repositories;
using FileTrader.Contracts.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrader.AppServices.Users.Services
{
    /// <inheridoc />
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Инициализирует экземпляр <see cref="UserService"/>.
        /// </summary>
        /// <param name="userRepository"></param>
        public UserService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<UserDTO>> GetUsersAsync(CancellationToken cancellationToken)
        {
            return await _userRepository.GetAllAsync(cancellationToken);
        }
    }
}
