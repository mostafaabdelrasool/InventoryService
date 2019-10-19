using Inventory.Application.Customer.query;
using Inventory.Application.Interfaces;
using Inventory.Application.Product.command;
using Inventory.Application.Product.query;
using Inventory.Domain;
using Inventory.Domain.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Inventory.Application.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //services.AddTransient<IService<Products>, Service<Products>>();
            services.AddTransient<IProductSizesQueryService, ProductSizesQueryService>();
            services.AddTransient<IUpdateStockCommand, UpdateStockCommand>();
            services.AddTransient<ICustomerQueryService, CustomerQueryService>();
            services.AddTransient<IProductQueryService, ProductQueryService>();
            services.AddTransient(typeof(IService<>), typeof(Service<>));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
