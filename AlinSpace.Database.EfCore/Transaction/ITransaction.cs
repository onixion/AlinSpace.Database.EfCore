using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AlinSpace.Database.EfCore
{
    /// <summary>
    /// Represents the transaction interface.
    /// </summary>
    public interface ITransaction : IDisposable
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        DbContext Context { get; }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <returns>Repository.</returns>
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;

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
