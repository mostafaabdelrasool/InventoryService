using Inventory.Application.Customer.query;
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
            //services.AddTransient<IService<Products>, Service<Products>>();
            //services.AddTransient<IService<Categories>, Service<Categories>>();
            //services.AddTransient<IService<ModelDesign>, Service<ModelDesign>>();
            services.AddTransient<ICustomerQueryService, CustomerQueryService>();
            services.AddTransient(typeof(IService<>), typeof(Service<>));

            return services;
        }
    }
}
