﻿using Inventory.Domain;
using Inventory.Domain.Models;
using Inventory.Persistance.Interfaces;
using Inventory.Persistance.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Persistance.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}
