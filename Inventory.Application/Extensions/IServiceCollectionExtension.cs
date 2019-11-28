using Inventory.Application.Customer.query;
using Inventory.Application.Interfaces;
using Inventory.Application.Order.Query;
using Inventory.Application.Order.Service;
using Inventory.Application.Product.query;
using Inventory.Domain;
using Inventory.Domain.Models;
using Inventory.Domain.Order;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Inventory.Application.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProductSizesQueryService, ProductSizesQueryService>();
            services.AddScoped<ICustomerQueryService, CustomerQueryService>();
            services.AddScoped<IProductQueryService, ProductQueryService>();
            services.AddScoped(typeof(IService<>), typeof(Service<>));
            services.AddScoped<IOrderEventService, OrderEventService>();
            services.AddScoped<IQueryOrderEvent, QueryOrderEvent>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
