using Application.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configuration
{
    public static class ControllersInstaller
    {
        public static IServiceCollection InstallControllers(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            return services;
        }
    }
}