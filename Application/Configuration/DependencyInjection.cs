using System;
using System.Linq;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configuration
{
    public static class DependencyInjection
    {
        /*
        Commented section stays as more like an example. As the order is important in more complex
        methods it shouldn't have been installed via assembly.
        */
        public static IServiceCollection InstallMediatRAndSignalR(this IServiceCollection services, IConfiguration configuration)
        {
            /*
            var installersInAssembly = typeof(ApplicationAssemblyEndpoint).Assembly.ExportedTypes
                .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>()
                .ToList();

            foreach(var installer in installersInAssembly)
            {
                installer.InstallServices(configuration, services);
            }
            */     

            services.AddMediatR(typeof(ApplicationAssemblyEndpoint));
            services.AddSignalR();

            return services;
        }
    }

}