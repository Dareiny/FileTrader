using FileTrader.DbMigrator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddServices(hostContext.Configuration);
    }).Build();

await MigrateDataBaseAsync(host.Services);

await host.StartAsync();

static async Task MigrateDataBaseAsync(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<MigrationDbContext>();
    await context.Database.MigrateAsync();
}