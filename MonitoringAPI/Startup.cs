using Core.Application;
using Features.EventLog;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MonitoringAPI.Extensions;

namespace MonitoringAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        private IServiceCollection services;

        public void ConfigureServices(IServiceCollection services)
        {
            this.services = services;
            services.AddDistributedMemoryCache();
            services.AddControllers();
            services.AddMemoryCache();
            services.AddAuthentication();
            services.AddApplication(Configuration);
            services.AddServices(Configuration);
            services.AddAuth();
            services.AddEventLog();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
