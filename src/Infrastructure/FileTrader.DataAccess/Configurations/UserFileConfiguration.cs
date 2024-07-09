using FileTrader.Domain.Files.Entity;
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
    /// <summary>
    /// Файл конфигурации сущности файла.
    /// </summary>
    public class UserFileConfiguration : IEntityTypeConfiguration<UserFile>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<UserFile> builder)
        {
            builder.ToTable("UserFiles").HasKey(t => t.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Content).IsRequired().HasMaxLength(255);

        }
    }
}
