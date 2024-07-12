using FileTrader.DataAccess;

public class MigrationDbContext : ApplicationDbContext
{
    public MigrationDbContext(Microsoft.EntityFrameworkCore.DbContextOptions options) : base(options)
    {
    }
}