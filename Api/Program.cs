using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Concrete;
using Api.Data.Concrete.EntityFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>
                ();
                var logger = loggerFactory.CreateLogger<Program>();

                try
                {
                    var context = services.GetRequiredService<CustomerContext>();
                    await context.Database.MigrateAsync();
                    await SeedCustomers.SeedAsync(context, loggerFactory);
                    logger.LogInformation("Migration is applied");
                }
                catch (System.Exception ex)
                {
                    logger.LogError(ex, "An error occured during migration");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
