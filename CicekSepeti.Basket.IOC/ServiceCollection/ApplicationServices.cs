using CicekSepeti.Basket.Caching;
using CicekSepeti.Basket.Data.Repositories;
using CicekSepeti.Basket.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CicekSepeti.Basket.IOC.ServiceCollection
{
    public static class ApplicationServices
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSingleton(typeof(ICacheService<>), typeof(CacheService<>));

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IBasketService, BasketService>();
            services.AddSingleton<IProductRepository, ProductMongoRepository>();

            return services;
        }
    }
}
