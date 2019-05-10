using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Inventory.Persistance.Extensions
{
    public static class EntityTypeConfigurationExtensions
    {
        private static readonly MethodInfo EntityMethod = typeof(ModelBuilder).GetTypeInfo().GetMethods()
            .Single(x => x.Name == "Entity"
            && x.IsGenericMethod && x.GetParameters().Length == 0);
        private static readonly IDictionary<Assembly, IEnumerable<Type>> TypesPerAssembly = new Dictionary<Assembly, IEnumerable<Type>>();

        private static Type GetEntityType(Type type)
        {
            Type interfaceType = type.GetInterfaces().First(x => x.GetTypeInfo().IsGenericType && x.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));
            return interfaceType.GenericTypeArguments[0];
        }

        private static ModelBuilder ApplyConfiguration<T>(this ModelBuilder modelBuilder, IEntityTypeConfiguration<T> configuration) where T : class
        {
            Type entityType = GetEntityType(configuration.GetType());

            dynamic entityTypeBuilder = EntityMethod
                .MakeGenericMethod(entityType)
                .Invoke(modelBuilder, new object[0]);

            configuration.Configure(entityTypeBuilder);

            return modelBuilder;
        }

        public static ModelBuilder UseEntityTypeConfiguration<T>(this ModelBuilder modelBuilder)
        {
            IEnumerable<Type> configurationTypes;
            var CurrentAssembly = typeof(T).Assembly;
            if (!TypesPerAssembly.TryGetValue(CurrentAssembly, out configurationTypes))
            {
                TypesPerAssembly[CurrentAssembly] = configurationTypes = CurrentAssembly
                    .GetExportedTypes()
                    .Where(x => x.GetTypeInfo().IsClass && !x.GetTypeInfo().IsAbstract && x.GetInterfaces().Any(y => y.GetTypeInfo().IsGenericType && y.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
            }
            IEnumerable<dynamic> configurations = configurationTypes.Select(Activator.CreateInstance);

            foreach (dynamic configuration in configurations)
            {
                ApplyConfiguration(modelBuilder, configuration);
            }

            return modelBuilder;
        }
    }
}
