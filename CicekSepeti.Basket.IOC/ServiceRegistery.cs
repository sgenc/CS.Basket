using CicekSepeti.Basket.IOC.ServiceCollection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CicekSepeti.Basket.IOC
{
    public class ServiceRegistry
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterApplicationServices(configuration);
        }
    }
}
