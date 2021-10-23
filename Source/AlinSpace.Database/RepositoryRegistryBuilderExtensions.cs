using System;

namespace AlinSpace.Database
{
    public static class RepositoryRegistryBuilderExtensions
    {
        public static RepositoryRegistryBuilder Register<TEntity>(
            this RepositoryRegistryBuilder repositoryRegistryBuilder, 
            Func<IRepository<TEntity>> repositoryProvider) 
            where TEntity : class
        {
            var repositoryKey = new RepositoryKey(typeof(TEntity));
            repositoryRegistryBuilder.Register(new RepositoryRegistration(repositoryKey, new Lazy<object>(repositoryProvider)));

            return repositoryRegistryBuilder;
        }
    }
}
