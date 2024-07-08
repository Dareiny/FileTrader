using AutoMapper;
using AutoMapper.QueryableExtensions;
using FileTrader.AppServices.Users.Repositories;
using FileTrader.Contracts.Users;
using FileTrader.Domain.Users.Entity;
using FileTrader.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        public async Task AddAsync(User entity,CancellationToken cancellationToken)
        {
            await _repository.AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(id, cancellationToken);
        }


        /// <inheritdoc />
        public async Task<IEnumerable<UserDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            //var users = UserList();

            return await _repository.GetAll()
                .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<UserDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _repository.GetAll().Where(s => s.Id == id)
                .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<UserDTO>> GetFiltered(Expression<Func<User, bool>> predicate,CancellationToken cancellationToken)
        {
            return await _repository.GetAll().Where(predicate)
                .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task UpdateAsync(UserDTO entity, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(entity.Id, cancellationToken);
            user.UserName = entity.UserName;
            user.UserEmail = entity.UserEmail;
            user.Password = entity.Password;
            await _repository.UpdateAsync(user, cancellationToken);
        }
    }
}
