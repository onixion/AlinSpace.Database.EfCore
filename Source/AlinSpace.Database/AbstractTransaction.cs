using System;
using System.Threading;
using System.Threading.Tasks;

namespace AlinSpace.Database
{
    /// <summary>
    /// Represents the abstract implementation of <see cref="ITransaction"/>.
    /// </summary>
    public abstract class AbstractTransaction : ITransaction
    {
        private readonly RepositoryRegistry repositoryRegistry;
    
        /// <summary>
        /// Constructor.
        /// </summary>
        public AbstractTransaction(RepositoryRegistry repositoryRegistry)
        {
            this.repositoryRegistry = repositoryRegistry ?? throw new ArgumentNullException(nameof(repositoryRegistry));
        }

        /// <summary>
        /// Get repository.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <returns>Repository.</returns>
        public IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class
        {
            return repositoryRegistry.GetRepository<TEntity, TKey>();
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
    }
}
