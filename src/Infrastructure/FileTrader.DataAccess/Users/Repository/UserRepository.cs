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

        public UserRepository(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(UserDTO entity,CancellationToken cancellationToken)
        {
            var result = new User()
            {
                Id = entity.Id,
                UserName = entity.UserName,
                UserEmail = entity.UserEmail,
                Password = entity.Password,
            };
            await _repository.AddAsync(result, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(id, cancellationToken);
        }


        /// <inheritdoc />
        public async Task<IEnumerable<UserDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            //var users = UserList();

            var users = await _repository.GetAll().ToListAsync(cancellationToken);

            return await Task.Run(()=>users.Select(u => new UserDTO
            {
                Id = u.Id,
                UserName = u.UserName,
                UserEmail = u.UserEmail
            }),cancellationToken);
        }

        public ValueTask<UserDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = _repository.GetByIdAsync(id, cancellationToken).Result;


            var result = new UserDTO()
            {
                Id = user.Id,
                UserName = user.UserName,
                UserEmail = user.UserEmail,

            };
            return new ValueTask<UserDTO>(result);
        }

        public async Task<IEnumerable<UserDTO>> GetFiltered(Expression<Func<User, bool>> predicate,CancellationToken cancellationToken)
        {
            var users = _repository.GetFiltered(predicate).ToList();


            return await Task.Run(() => users.Select(u => new UserDTO
            {
                Id = u.Id,
                UserName = u.UserName,
                UserEmail = u.UserEmail,
            }), cancellationToken);

        }

        public async Task UpdateAsync(UserDTO entity, CancellationToken cancellationToken)
        {
            var user = _repository.GetByIdAsync(entity.Id, cancellationToken).Result;
            user.UserName = entity.UserName;
            user.UserEmail = entity.UserEmail;
            user.Password = entity.Password;
            await _repository.UpdateAsync(user, cancellationToken);
        }
    }
}
