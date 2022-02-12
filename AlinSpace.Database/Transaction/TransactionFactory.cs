using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AlinSpace.Database
{
    /// <summary>
    /// Represents the factory.
    /// </summary>
    public static class TransactionFactory
    {
        private static readonly IDictionary<Type, IList<PropertyInfo>> dbContextMap = new Dictionary<Type, IList<PropertyInfo>>();

        /// <summary>
        /// Create transaction.
        /// </summary>
        /// <param name="dbContext">DbContext.</param>
        /// <returns>Transaction.</returns>
        public static ITransaction Create(DbContext dbContext)
        {
            var dbContextType = dbContext.GetType();

            if (!dbContextMap.TryGetValue(dbContextType, out var properties))
            {
                properties = dbContextType
                    .GetProperties()
                    .Where(x => x.PropertyType.IsGenericType ? x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) : false)
                    .ToList();

                dbContextMap[dbContextType] = properties;
            }

            var repositoryRegistry = new RepositoryRegistry();

            foreach (var property in properties)
            {
                var entityType = property.PropertyType.GenericTypeArguments.First();
                var repositoryType = typeof(Repository<>).MakeGenericType(entityType);

                var repositoryProvider = new Lazy<object>(() =>
                {
                    var dbSet = property.GetValue(dbContext);
                    return Activator.CreateInstance(repositoryType, new object[] { dbContext, dbSet });
                },
                true);

                repositoryRegistry.Register(entityType, repositoryProvider);
            }

            return new Transaction(dbContext, repositoryRegistry);
        }

        /// <summary>
        /// Create transaction.
        /// </summary>
        /// <param name="dbContextType">DbContext type.</param>
        /// <param name="options">DbContext options.</param>
        /// <returns>Transaction.</returns>
        public static ITransaction Create(Type dbContextType, DbContextOptions options = null)
        {
            var dbContext = (DbContext)Activator.CreateInstance(dbContextType, new object[] { options });
            return Create(dbContext);
        }

        /// <summary>
        /// Create transaction.
        /// </summary>
        /// <typeparam name="TDbContext">Type of database context.</typeparam>
        /// <param name="options">DbContext options.</param>
        /// <returns>Transaction.</returns>
        public static ITransaction Create<TDbContext>(DbContextOptions options = null) where TDbContext : DbContext
        {
            return Create(typeof(TDbContext), options);
        }
    }
}
