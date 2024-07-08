using FileTrader.DataAccess;
using Microsoft.EntityFrameworkCore;

public class MigrationDbContext : ApplicationDbContext
{
    public MigrationDbContext(Microsoft.EntityFrameworkCore.DbContextOptions options) : base(options)
    {
    }
}