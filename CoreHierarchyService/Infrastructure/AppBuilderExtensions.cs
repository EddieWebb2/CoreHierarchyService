using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;

namespace CoreHierarchyService.Infrastructure
{
    public static class AppBuilderExtensions
    {
        public static void BuildSwagger(this IApplicationBuilder app, ICoreHierarchyServiceConfiguration config)
        {
            app.UseSwagger();
            var asm = Assembly.GetExecutingAssembly();
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = asm.GetCustomAttributes(typeof(AssemblyProductAttribute), false)
                    .OfType<AssemblyProductAttribute>().FirstOrDefault()?.Product;
                c.SwaggerEndpoint($"/swagger/{config.SoftwareName}/swagger.json", config.SoftwareName);
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
