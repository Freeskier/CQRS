using Application.Configuration;
using FluentValidation;
using Infrastructure.PipelineBehaviors;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configuration
{
    public static class ValidatorsInstaller
    {
        public static IServiceCollection InstallValidators(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddValidatorsFromAssembly(typeof(InfrastructureAssemblyEndpoint).Assembly);

            return services;
        }
    }
}