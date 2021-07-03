using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AlinSpace.Database.Ef
{
    /// <summary>
    /// Represents an abstract implementation of <see cref="ITransaction"/> for EF.
    /// </summary>
    public abstract class AbstractTransaction : Database.AbstractTransaction
    {
        private readonly DbContext dbContext;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbContext">DB context.</param>
        public AbstractTransaction(DbContext dbContext) : base()
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Commits the changes.
        /// </summary>
        /// <return>The number of state entries written to the underlying database.</return>
        public override int Commit()
        {
            return dbContext.SaveChanges();
        }

        /// <summary>
        /// Commits the changes asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The number of state entries written to the underlying database.</returns>
        public override Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return dbContext.SaveChangesAsync(cancellationToken);
        }

        bool disposed;

        /// <summary>
        /// Dispose.
        /// </summary>
        public override void Dispose()
        {
            if (disposed)
                return;

            disposed = true;

            dbContext.Dispose();
        }

        /// <summary>
        /// Dispose asynchronously.
        /// </summary>
        public override ValueTask DisposeAsync()
        {
            if (disposed)
                return ValueTask.CompletedTask;

            disposed = true;

            return dbContext.DisposeAsync();
        }
    }
}
