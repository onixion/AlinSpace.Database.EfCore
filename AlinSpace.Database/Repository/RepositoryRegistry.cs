using System;
using System.Collections.Generic;

namespace AlinSpace.Database
{
    class RepositoryRegistry
    {
        private readonly IDictionary<Type, Lazy<object>> repositories = new Dictionary<Type, Lazy<object>>();

        public void Register(Type entityType, Lazy<object> repositoryProvider)
        {
            repositories[entityType] = repositoryProvider;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : IEntity
        {
            if (!repositories.TryGetValue(typeof(TEntity), out Lazy<object> repository))
                throw new Exception($"No repository found found for entity type {typeof(TEntity)}.");

            if (!(repository.Value is IRepository<TEntity> specificRepository))
                throw new Exception($"Registered repository with wrong type. Please check the repository registrations.");

            return specificRepository;
        }
    }
}
