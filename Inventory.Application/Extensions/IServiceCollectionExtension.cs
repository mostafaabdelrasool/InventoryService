using Inventory.Application.Customer.query;
using Inventory.Application.Interfaces;
using Inventory.Application.Order.Commands;
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
            services.AddTransient<IOrderSavedCommand, OrderSavedCommand>();
            services.AddScoped<IProductSizesQueryService, ProductSizesQueryService>();
            services.AddScoped<IUpdateStockCommand, UpdateStockCommand>();
            services.AddScoped<ICustomerQueryService, CustomerQueryService>();
            services.AddScoped<IProductQueryService, ProductQueryService>();
            services.AddScoped(typeof(IService<>), typeof(Service<>));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
