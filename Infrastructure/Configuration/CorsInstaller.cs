using Application.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configuration
{
    public static class CorsInstaller
    {
        public static IServiceCollection InstallCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                    builder =>
                    {
                        builder.AllowAnyHeader()
                                .AllowCredentials()
                                .AllowAnyMethod()
                                .WithOrigins("http://localhost:4200");
                    });
            });

            return services;
        }
    }
}