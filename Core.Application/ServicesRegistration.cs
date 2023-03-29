using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Core.Application
{
    public static class ServicesRegistration
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            //var featureAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.StartsWith("Feature")).ToList();
            //featureAssemblies.Add(Assembly.GetExecutingAssembly());
        }
    }
}