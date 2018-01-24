using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Test.Core.Domain;

namespace Test.Data.Mapping
{
    public static class ModelBuilderExtenions
    {
        private static IEnumerable<Type> GetMappingTypes(this Assembly assembly, Type mappingInterface)
        {
            return assembly.GetTypes().Where(x => !x.IsAbstract && !x.IsGenericType && !x.IsInterface && x.GetInterfaces().Any(y => y == mappingInterface));
        }

        public static void AddEntityConfigurationsFromAssembly(this ModelBuilder modelBuilder, Assembly assembly)
        {
            var mappingTypes = assembly.GetMappingTypes(typeof(ISelfEntityMappingConfiguration));
            foreach (var config in mappingTypes.Select(Activator.CreateInstance).Cast<ISelfEntityMappingConfiguration>())
            {
                config.Map(modelBuilder);
            }
        }
    }
}
