using Inventory.Application.Interfaces;
using Inventory.Domain;
using Inventory.Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Application.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IService<Products>, Service<Products>>();
            services.AddTransient<IService<Categories>, Service<Categories>>();

            return services;
        }
    }
}
