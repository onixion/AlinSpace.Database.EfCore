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
        /// <param name="dbContext">Database context.</param>
        public AbstractTransaction(DbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Commits the changes.
        /// </summary>
        public override void Commit()
        {
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Commits the changes asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        public override async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
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
    }
}
