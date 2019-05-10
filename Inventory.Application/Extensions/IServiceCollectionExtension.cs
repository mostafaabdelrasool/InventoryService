using Inventory.Application.Interfaces;
using Inventory.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Application.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IService<Entity>, Service<Entity>>();

            return services;
        }
    }
}
