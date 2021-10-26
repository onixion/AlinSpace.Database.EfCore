using System;

namespace AlinSpace.Database
{
    /// <summary>
    /// Extensions for <see cref="RepositoryRegistryBuilder"/>.
    /// </summary>
    public static class RepositoryRegistryBuilderExtensions
    {
        /// <summary>
        /// Register repository provider.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="repositoryRegistryBuilder">Repository registry builder.</param>
        /// <param name="repositoryProvider">Repository provider.</param>
        public static RepositoryRegistryBuilder Register<TEntity>(
            this RepositoryRegistryBuilder repositoryRegistryBuilder, 
            Func<IRepository<TEntity>> repositoryProvider) 
            where TEntity : class
        {
            var repositoryKey = typeof(TEntity);
            repositoryRegistryBuilder.Register(new RepositoryRegistration(repositoryKey, new Lazy<object>(repositoryProvider)));

            return repositoryRegistryBuilder;
        }
    }
}
