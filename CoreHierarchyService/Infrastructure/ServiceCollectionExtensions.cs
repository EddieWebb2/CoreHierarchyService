using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CoreHierarchyService.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        // TODO - Placeholder as i know i will need to extend the service collection!
        //public static void ConfigureDependencies(this IServiceCollection services)
        //{
        //    services.AddTransient<UserCqrs or something>();
        //}

        public static void ConfigureSwagger(this IServiceCollection services, ICoreHierarchyServiceConfiguration config)
        {
            var asm = Assembly.GetExecutingAssembly();
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc(config.SoftwareName, new OpenApiInfo
                {
                    // TODO - Use reflection here to get the appropriate types.. i may do away with this...
                    Description = asm.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false).OfType<AssemblyDescriptionAttribute>().FirstOrDefault()?.Description,
                    Title = asm.GetCustomAttributes(typeof(AssemblyProductAttribute), false).OfType<AssemblyProductAttribute>().FirstOrDefault()?.Product,
                    Version = asm.GetName().Version.ToString()
                });
                c.DescribeAllEnumsAsStrings();
            });
        }
    }
}
