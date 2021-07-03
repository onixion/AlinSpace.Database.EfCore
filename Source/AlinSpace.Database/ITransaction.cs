using System;
using System.Threading;
using System.Threading.Tasks;

namespace AlinSpace.Database
{
    /// <summary>
    /// Represents the transaction interface.
    /// </summary>
    public interface ITransaction : IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// Get repository.
        /// </summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <returns>Repository.</returns>
        IRepository<TModel, TKey> GetRepository<TModel, TKey>() where TModel : class;

        /// <summary>
        /// Commits the changes.
        /// </summary>
        /// <return>The number of state entries written to the underlying database.</return>
        int Commit();

        /// <summary>
        /// Commits the changes asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The number of state entries written to the underlying database.</returns>
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
