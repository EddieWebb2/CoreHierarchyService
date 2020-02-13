using CoreHierarchyService.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreHierarchyService
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                
                // Configures my service!
                .ConfigureServices((hostContext, services) =>
                {
                    // This is the DI for the Worker class! Very cool!
                    IConfiguration configuration = hostContext.Configuration;
                    CoreHierarchyServiceConfiguration config = configuration
                        .GetSection("CoreHierarchyServiceConfiguration").Get<CoreHierarchyServiceConfiguration>();
                    services.AddSingleton(config);

                    services.AddHostedService<Worker>();
                })
                
                // Configures my Web Api!
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
