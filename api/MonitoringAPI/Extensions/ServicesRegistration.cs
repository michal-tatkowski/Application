using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Core.Application.Wrappers;
using Core.Application;

namespace MonitoringAPI.Extensions
{
    public static class ServicesRegistration
    {
        public static void AddServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddOptions();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
        }
    }
}
