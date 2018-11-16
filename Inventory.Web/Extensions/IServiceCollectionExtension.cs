using Inventory.Infrastructure.Interfaces;
using Inventory.Infrastructure.Models;
using Inventory.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Web.Extensions
{
  public static class IServiceCollectionExtension
  {
    public static IServiceCollection AddWeb(this IServiceCollection services)
    {
      services.AddTransient<IRepository<Categories>, Repository<Categories>>();
      return services;
    }
  }
}
