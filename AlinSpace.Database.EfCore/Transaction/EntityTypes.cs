using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlinSpace.Database.EfCore
{
    public static class EntityTypes
    {
        public static IEnumerable<Type> GetFromDbContextType(Type dbContextType)
        {
            if (!dbContextType.IsSubclassOf(typeof(DbContext)))
            {
                throw new ArgumentException(nameof(dbContextType));
            }

            return dbContextType
                .GetProperties()
                .Where(x => x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .Select(x => x.PropertyType.GenericTypeArguments.First())
                .ToList();
        }
    }
}
