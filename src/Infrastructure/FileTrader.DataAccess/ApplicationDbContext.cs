using FileTrader.DataAccess.Configurations;
using FileTrader.Domain.Files.Entity;
using FileTrader.Domain.Users.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrader.DataAccess
{

    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Сущность пользователь
        /// </summary>
        public DbSet<User>  Users {get;set;}
        /// <summary>
        /// Сущность файлы пользователя
        /// </summary>
        public DbSet<UserFile> UserFiles {get;set;}


        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
