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
        private readonly IDictionary<(Type, Type), Lazy<object>> repositories = new Dictionary<(Type, Type), Lazy<object>>();

        /// <summary>
        /// Constructor.
        /// </summary>
        public RepositoryRegistry(IEnumerable<RepositoryRegistration> registrations)
        {
            foreach(var registration in registrations)
            {
                repositories[(registration.Key.EntityType, registration.Key.KeyType)] = registration.RepositoryProvider;
            }
        }

        /// <summary>
        /// Get repository.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <returns>Repository.</returns>
        public IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class
        {
            if (!repositories.TryGetValue((typeof(TEntity), typeof(TKey)), out Lazy<object> repository))
                throw new Exception($"No repository found found for entity type {typeof(TEntity)} and key type {typeof(TKey)}.");

            if (!(repository.Value is IRepository<TEntity, TKey> specificRepository))
                throw new Exception($"Registered repository with wrong type. Please check the repository registrations.");

            return specificRepository;
        }
    }
}
