using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Configuration;

namespace Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.InstallDb(configuration);
            services.InstallCors(configuration);
            services.InstallControllers(configuration);
            services.InstallServices(configuration);
            services.InstallMediatRAndSignalR(configuration);
            services.InstallValidators(configuration);
            services.InstallAuth(configuration);
            services.InstallSwagger(configuration);

            return services;
        }
    }
}