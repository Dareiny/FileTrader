using FileTrader.Domain.Files.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileTrader.DataAccess.Configurations
{
    /// <summary>
    /// Файл конфигурации сущности файла.
    /// </summary>
    public class FileConfiguration : IEntityTypeConfiguration<EFile>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<EFile> builder)
        {
            builder.ToTable("UserFiles").HasKey(t => t.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Content).IsRequired().HasMaxLength(255);


        }
    }
}
