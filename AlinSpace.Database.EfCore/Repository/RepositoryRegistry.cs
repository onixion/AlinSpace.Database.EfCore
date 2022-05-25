using System;
using System.Collections.Concurrent;

namespace AlinSpace.Database.EfCore
{
    sealed class RepositoryRegistry
    {
        private readonly ConcurrentDictionary<Type, Lazy<object>> repositories = new ConcurrentDictionary<Type, Lazy<object>>();

        public void Register(Type entityType, Lazy<object> repositoryProvider)
        {
            repositories.AddOrUpdate(entityType, repositoryProvider, (entityType, currentRepositoryProvider) => repositoryProvider);
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity
        {
            if (!repositories.TryGetValue(typeof(TEntity), out Lazy<object> repository))
                throw new Exception($"No repository found found for entity type {typeof(TEntity)}.");

            if (!(repository.Value is IRepository<TEntity> specificRepository))
                throw new Exception($"Registered repository with wrong type. Please check the repository registrations.");

            return specificRepository;
        }
    }
}
