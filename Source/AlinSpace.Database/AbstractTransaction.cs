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
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <typeparam name="TKey">Type of the primary key.</typeparam>
        /// <param name="repositoryProvider">Repository provider.</param>
        protected void RegisterRepository<TModel, TKey>(Func<IRepository<TModel, TKey>> repositoryProvider) where TModel : class
        {
            repositories[(typeof(TModel), typeof(TKey))] = new Lazy<object>(() => repositoryProvider());
        }

        /// <summary>
        /// Get repository.
        /// </summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <returns>Repository.</returns>
        public IRepository<TModel, TKey> GetRepository<TModel, TKey>() where TModel : class
        {
            if (!repositories.TryGetValue((typeof(TModel), typeof(TKey)), out Lazy<object> repository))
                throw new Exception($"No repository found.");

            if (repository.Value is not IRepository<TModel, TKey> specificRepository)
                throw new Exception($"Repository type wrong.");

            return specificRepository;
        }

        /// <summary>
        /// Commits the changes.
        /// </summary>
        /// <return>The number of state entries written to the underlying database.</return>
        public abstract int Commit();

        /// <summary>
        /// Commits the changes asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The number of state entries written to the underlying database.</returns>
        public abstract Task<int> CommitAsync(CancellationToken cancellationToken = default);

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
