using AutoMapper;
using FileTrader.ComponentRegistrar.MapProfiles;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FileTrader.ComponentRegistrar
{
    public static class Registrar
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
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
            });
            config.AssertConfigurationIsValid();
            return config;
        }

    }
}
