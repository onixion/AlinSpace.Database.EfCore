using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AlinSpace.Database.Ef
{
    /// <summary>
    /// Represents the implementation of <see cref="ITransaction"/> for EF.
    /// </summary>
    public sealed class Transaction : AbstractTransaction
    {
        private readonly DbContext dbContext;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbContext">Database context.</param>
        public Transaction(DbContext dbContext, RepositoryRegistry repositoryRegistry) : base(repositoryRegistry)
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
