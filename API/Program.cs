using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.DataAccess;
using Infrastructure.Configuration;
using Application.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Infrastructure.Helpers;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            //await GenerateRandomUsers(args, host);

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        // private async static Task GenerateRandomUsers(string[] args, IHost host)
        // {
        //     using(var scope = host.Services.CreateScope())
        //     {
        //         var services = scope.ServiceProvider;

        //         try
        //         {
        //             var context = services.GetRequiredService<ApplicationDataContext>();
        //             var authService = services.GetRequiredService<IAuthenticationService>();

        //             //await context.Database.MigrateAsync();
        //             //await SeedUsers.Seed(context, authService);
        //             await SeedUsers.AddUrls(context);
        //         }
        //         catch(Exception ex)
        //         {
        //             var logger = services.GetRequiredService<ILogger<Program>>();
        //             logger.LogError(ex, "An error occurred during migration");
        //         }
        //     }
        // }
    }
}
