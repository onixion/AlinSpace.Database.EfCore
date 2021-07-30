using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AlinSpace.Database
{
    /// <summary>
    /// Represents an abstract implementation of <see cref="ITransaction"/>.
    /// </summary>
    public abstract class AbstractTransaction : ITransaction
    {
        /// <summary>
        /// Repository mappings.
        /// </summary>
        readonly IDictionary<(Type, Type), Lazy<object>> repositories = new Dictionary<(Type, Type), Lazy<object>>();

        /// <summary>
        /// Register repository.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TKey">Type of the primary key.</typeparam>
        /// <param name="repositoryProvider">Repository provider.</param>
        protected void RegisterRepository<TEntity, TKey>(Func<IRepository<TEntity, TKey>> repositoryProvider) where TEntity : class
        {
            repositories[(typeof(TEntity), typeof(TKey))] = new Lazy<object>(() => repositoryProvider());
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

        /// <summary>
        /// Commits the changes.
        /// </summary>
        public abstract void Commit();

        /// <summary>
        /// Commits the changes asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        public abstract Task CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Dispose.
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// Dispose asynchronously.
        /// </summary>
        public abstract ValueTask DisposeAsync();
    }
}
