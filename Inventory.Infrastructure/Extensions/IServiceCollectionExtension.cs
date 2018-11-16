using Inventory.Domain;
using Inventory.Infrastructure.Interfaces;
using Inventory.Infrastructure.Models;
using Inventory.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IRepository<Categories>, Repository<Categories>>();
            services.AddTransient<IRepository<Products>, Repository<Products>>();
            return services;
        }
    }
}
