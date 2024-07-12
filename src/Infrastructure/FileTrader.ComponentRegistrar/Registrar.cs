using AutoMapper;
using FileTrader.AppServices.Auth.Services;
using FileTrader.AppServices.UserFiles.Repositories;
using FileTrader.AppServices.UserFiles.Services;
using FileTrader.AppServices.Users.Repositories;
using FileTrader.AppServices.Users.Services;
using FileTrader.ComponentRegistrar.MapProfiles;
using FileTrader.DataAccess;
using FileTrader.DataAccess.UserFiles.Repository;
using FileTrader.DataAccess.Users.Repository;
using FileTrader.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileTrader.ComponentRegistrar
{
    public static class Registrar
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            // Add services to the container.
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddRazorPages();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("https://localhost:7002") // Укажите адрес вашего клиента
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });


            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddTransient<IUserFilesService, UserFilesService>();
            services.AddScoped<IUserFilesRepository, UserFilesRepository>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DbConnection")));
            services.AddScoped<DbContext>(s => s.GetRequiredService<ApplicationDbContext>());
            return services.ConfigureAutoMapper();
        }

        private static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            return services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));
        }

        private static MapperConfiguration GetMapperConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<FileProfile>();
            });
            config.AssertConfigurationIsValid();
            return config;
        }

    }
}
