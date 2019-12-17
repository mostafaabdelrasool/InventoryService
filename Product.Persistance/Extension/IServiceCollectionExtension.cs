
using Domain.Service.Repository;
using Microsoft.Extensions.DependencyInjection;
using Product.Persistance.Repository;

namespace Product.Persistance.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services)
        {
            services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            return services;
        }
    }
}
