using System;
using System.Collections.Generic;

namespace AlinSpace.Database
{
    /// <summary>
    /// Repository registry.
    /// </summary>
    public class RepositoryRegistry
    {
        /// <summary>
        /// Repository mappings.
        /// </summary>
        private readonly IDictionary<Type, Lazy<object>> repositories = new Dictionary<Type, Lazy<object>>();

        /// <summary>
        /// Constructor.
        /// </summary>
        public RepositoryRegistry(IEnumerable<RepositoryRegistration> registrations)
        {
            foreach(var registration in registrations)
            {
                repositories[registration.Key.EntityType] = registration.RepositoryProvider;
            }
        }

        /// <summary>
        /// Get repository.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <returns>Repository.</returns>
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (!repositories.TryGetValue(typeof(TEntity), out Lazy<object> repository))
                throw new Exception($"No repository found found for entity type {typeof(TEntity)}.");

            if (!(repository.Value is IRepository<TEntity> specificRepository))
                throw new Exception($"Registered repository with wrong type. Please check the repository registrations.");

            return specificRepository;
        }
    }
}
