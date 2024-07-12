using AutoMapper;
using AutoMapper.QueryableExtensions;
using FileTrader.AppServices.Specifications;
using FileTrader.AppServices.Users.Repositories;
using FileTrader.Contracts.General;
using FileTrader.Contracts.Users;
using FileTrader.Domain.Users.Entity;
using FileTrader.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace FileTrader.DataAccess.Users.Repository
{
    /// <inheritdoc />
    public class UserRepository : IUserRepository
    {

        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;
        public UserRepository(IRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddAsync(User entity, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(id, cancellationToken);
        }


        /// <inheritdoc />
        //public async Task<ResultWithPagination<UserDTO>> GetAllAsync(GetAllUsersRequest request, CancellationToken cancellationToken)
        //{
        //    var result = new ResultWithPagination<UserDTO>();

        //    var query = _repository.GetAll();

        //    var elementsCount = await query.CountAsync(cancellationToken);
        //    result.AvailablePages = elementsCount/request.BatchSize;
        //    if (elementsCount % request.BatchSize > 0) result.AvailablePages++;

        //    var paginationQuery = await query
        //        .OrderBy(user => user.Id)
        //        .Skip(request.BatchSize * (request.PageNumber - 1))
        //        .Take(request.BatchSize)
        //        .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
        //        .ToListAsync();

        //    result.Result = paginationQuery;
        //    return result;
        //}

        public async Task<ResultWithPagination<UserInfoDTO>> GetAllBySpecification(PaginationRequest request, Specification<User> specification, CancellationToken cancellationToken)
        {
            var result = new ResultWithPagination<UserInfoDTO>();

            var query = _repository.GetAll().Where(specification.ToExpression());

            var elementsCount = await query.CountAsync(cancellationToken);
            result.AvailablePages = elementsCount / request.BatchSize;
            if (elementsCount % request.BatchSize > 0) result.AvailablePages++;

            var paginationQuery = await query
               .OrderBy(user => user.Id)
               .Skip(request.BatchSize * (request.PageNumber - 1))
               .Take(request.BatchSize)
               .ProjectTo<UserInfoDTO>(_mapper.ConfigurationProvider)
               .ToListAsync();

            result.Result = paginationQuery;
            return result;
        }



        public async Task<UserDTO> GetByIdAsync(Specification<User> specification, CancellationToken cancellationToken)
        {
            return await _repository.GetAll().Where(specification.ToExpression())
                .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        }

        public async Task<UserDTO> GetByNameAsync(Specification<User> specification, CancellationToken cancellationToken)
        {
            return await _repository.GetAll().Where(specification.ToExpression())
                .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(UserDTO entity, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(entity.Id, cancellationToken);
            user.Login = entity.Login;
            user.UserEmail = entity.UserEmail;
            user.Password = entity.Password;
            await _repository.UpdateAsync(user, cancellationToken);
        }
    }
}
