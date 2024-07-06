using FileTrader.AppServices.Users.Repositories;
using FileTrader.Contracts.Users;
using FileTrader.Domain.Users.Entity;
using FileTrader.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
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
 
        private readonly IRepository<User> _repository;


        /// <inheritdoc />
        public async Task<IEnumerable<UserDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            //var users = UserList();

            var users = await _repository.GetAll().ToListAsync(cancellationToken);

            return await Task.Run(()=>users.Select(u => new UserDTO
            {
                Id = u.Id,
                UserName = u.UserName,
                UserEmail = u.UserEmail,
            }),cancellationToken);
        }
    }
}
