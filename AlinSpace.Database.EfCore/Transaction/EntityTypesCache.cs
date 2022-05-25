using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AlinSpace.Database.EfCore
{
    public static class EntityTypesCache
    {
        private static readonly ConcurrentDictionary<Type, IEnumerable<Type>> _entityTypesCache = new ConcurrentDictionary<Type, IEnumerable<Type>>();

        public static IEnumerable<Type> GetFromDbContextType(Type dbContextType)
        {
            if (!_entityTypesCache.TryGetValue(dbContextType, out var entityTypes))
            {
                entityTypes = EntityTypes.GetFromDbContextType(dbContextType);
                _entityTypesCache.AddOrUpdate(dbContextType, entityTypes, (key, current) => entityTypes);
            }

            return entityTypes;
        }
    }
}
