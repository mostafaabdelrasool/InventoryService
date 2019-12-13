using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Product.Web.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Web.Extension
{
    public static class IServiceCollectionExtension
    {
        public static IApplicationBuilder AddSeed(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var seed = serviceScope.ServiceProvider.GetService<InsertProductFromSheet>();
                seed.Insert().GetAwaiter().GetResult(); ;
            }
            return app;
        }
    }
}
