using Application.Configuration;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configuration
{
    public static class DbInstaller
    {
        public static IServiceCollection InstallDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDataContext>(x => x
                .UseLazyLoadingProxies()
                .UseSqlite(configuration
                .GetConnectionString("DefaultConnection"), 
                    x => x.MigrationsAssembly("API")));

            return services;
        }
    }
}