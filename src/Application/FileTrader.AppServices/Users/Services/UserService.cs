using AutoMapper;
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
        private readonly IMapper _mapper;


        /// <summary>
        /// Инициализирует экземпляр <see cref="UserService"/>.
        /// </summary>
        /// <param name="userRepository"></param>
        public UserService(IUserRepository userRepository, IMapper mapper) 
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(CreateUserRequest request, CancellationToken cancellationToken)
        {
           var entity = _mapper.Map<User>(request);
           
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
