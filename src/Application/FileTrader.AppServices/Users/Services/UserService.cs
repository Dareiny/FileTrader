using AutoMapper;
using FileTrader.AppServices.Specifications;
using FileTrader.AppServices.Users.Repositories;
using FileTrader.AppServices.Users.Specifications;
using FileTrader.Contracts.General;
using FileTrader.Contracts.Users;
using FileTrader.Domain.Users.Entity;

namespace FileTrader.AppServices.Users.Services
{
    /// <inheritdoc cref="IUserService"/>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        /// <summary>
        /// Инициализирует экземпляр <see cref="UserService"/>.
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="mapper"></param>
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

        public async Task<ResultWithPagination<UserInfoDTO>> GetUsersAsync(PaginationRequest request, CancellationToken cancellationToken)
        {
            var specification = new TrueSpecification<User>();
            return await _userRepository.GetAllBySpecification(request, specification, cancellationToken);
        }

        public async Task<UserDTO> GetUserByNameAsync(UsersByNameRequest request2, CancellationToken cancellationToken)
        {
            var specification = new ByNameSpecification(request2.Login);

            return await _userRepository.GetByNameAsync(specification, cancellationToken);
        }

        public async Task UpdateAsync(UserDTO entity, CancellationToken cancellationToken)
        {
            await _userRepository.UpdateAsync(entity, cancellationToken);
        }
    }
}
