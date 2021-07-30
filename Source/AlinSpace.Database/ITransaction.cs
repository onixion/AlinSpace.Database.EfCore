using System;
using System.Threading;
using System.Threading.Tasks;

namespace AlinSpace.Database
{
    /// <summary>
    /// Represents the transaction interface.
    /// </summary>
    public interface ITransaction : IDisposable
    {
        /// <summary>
        /// Get repository.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <returns>Repository.</returns>
        IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class;

        /// <summary>
        /// Commits the changes.
        /// </summary>
        void Commit();

        /// <summary>
        /// Commits the changes asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
