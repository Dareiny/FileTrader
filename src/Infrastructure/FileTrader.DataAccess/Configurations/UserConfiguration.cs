using FileTrader.Domain.Users.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrader.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users").HasKey(t => t.Id);
            builder.Property(x => x.Login).IsRequired().HasMaxLength(20);
            builder.Property(x => x.UserEmail).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(20);

        }
    }
}
