using Microsoft.Extensions.DependencyInjection;
using Product.Application.Product.query;
using System.Reflection;

namespace Product.Application.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProductSizesQueryService, ProductSizesQueryService>();
            services.AddScoped<IProductQueryService, ProductQueryService>();
            return services;
        }
    }
}
