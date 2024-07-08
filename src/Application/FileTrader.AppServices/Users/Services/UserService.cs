using FileTrader.AppServices.Users.Repositories;
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

        public async Task<Guid> AddAsync(UserDTO entity, CancellationToken cancellationToken)
        {
           entity.Id = Guid.NewGuid();
           await _userRepository.AddAsync(entity, cancellationToken);

           return entity.Id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _userRepository.DeleteAsync(id, cancellationToken);
        }

        public async ValueTask<UserDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _userRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<UserDTO>> GetFiltered(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _userRepository.GetFiltered(predicate, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<UserDTO>> GetUsersAsync(CancellationToken cancellationToken)
        {
            return await _userRepository.GetAllAsync(cancellationToken);
        }

        public async Task UpdateAsync(UserDTO entity, CancellationToken cancellationToken)
        {
            await _userRepository.UpdateAsync(entity, cancellationToken);
        }
    }
}
