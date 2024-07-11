using AutoMapper;
using FileTrader.AppServices.Specifications;
using FileTrader.AppServices.Users.Repositories;
using FileTrader.AppServices.Users.Specifications;
using FileTrader.Contracts.General;
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
            var specification = new ByIdSpecification(id);
            return await _userRepository.GetByIdAsync(specification, cancellationToken);
        }

        public async Task<ResultWithPagination<UserDTO>> GetUsersAsync(PaginationRequest request, CancellationToken cancellationToken)
        {
            var specification = new TrueSpecification<User>();
            return await _userRepository.GetAllBySpecification(request, specification, cancellationToken);
        }

        public async Task<ResultWithPagination<UserDTO>> GetUserByNameAsync(UsersByNameRequest request2, CancellationToken cancellationToken)
        {
            var request1 = new PaginationRequest { PageNumber = 1, BatchSize = 1 };
            var specification = new ByNameSpecification(request2.Login);

            return await _userRepository.GetAllBySpecification(request1 ,specification, cancellationToken);
        }

        public async Task UpdateAsync(UserDTO entity, CancellationToken cancellationToken)
        {
            await _userRepository.UpdateAsync(entity, cancellationToken);
        }
    }
}
