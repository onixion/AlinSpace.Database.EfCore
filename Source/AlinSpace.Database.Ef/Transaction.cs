using AlinSpace.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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
        /// <param name="repositoryRegistry">Repository registry.</param>
        public Transaction(DbContext dbContext, RepositoryRegistry repositoryRegistry) : base(repositoryRegistry)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        void PreCommit()
        {
            var entries = dbContext.ChangeTracker
                .Entries()
                .Where(
                    e => e.Entity is IEntity &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = entry.Entity as IEntity;

                entity.ModificationTimestamp = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entity.CreationTimestamp = DateTime.UtcNow;
                }
            }
        }

        /// <summary>
        /// Commits the changes.
        /// </summary>
        public override void Commit()
        {
            PreCommit();
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Commits the changes asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        public override async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            PreCommit();
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
