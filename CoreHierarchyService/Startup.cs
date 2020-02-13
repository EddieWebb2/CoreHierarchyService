
using CoreHierarchyService.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreHierarchyService
{
    public class Startup
    {
        private ICoreHierarchyServiceConfiguration _config;
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            _config = new CoreHierarchyServiceConfiguration();
            Configuration.Bind("CoreHierarchyServiceConfiguration", _config);
            services.AddSingleton(_config);

            services.ConfigureSwagger(_config);

            services.AddControllers();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.BuildSwagger(_config);

            app.UseHttpsRedirection();

            app.UseRouting();

            // TODO - Add authorisation layer... maybe IdentityServer 4
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}