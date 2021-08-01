using System;

namespace AlinSpace.Database
{
    public static class RepositoryRegistryBuilderExtensions
    {
        public static RepositoryRegistryBuilder Register<TEntity, TKey>(
            this RepositoryRegistryBuilder repositoryRegistryBuilder, 
            Func<IRepository<TEntity, TKey>> repositoryProvider) 
            where TEntity : class
        {
            var repositoryKey = new RepositoryKey(typeof(TEntity), typeof(TKey));
            repositoryRegistryBuilder.Register(new RepositoryRegistration(repositoryKey, new Lazy<object>(repositoryProvider)));

            return repositoryRegistryBuilder;
        }
    }
}
