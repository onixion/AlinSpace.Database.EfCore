using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AlinSpace.Database.EfCore
{
    internal class Transaction : ITransaction
    {
        private readonly RepositoryRegistry repositoryRegistry;
        
        public Transaction(DbContext dbContext, RepositoryRegistry repositoryRegistry)
        {
            Context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.repositoryRegistry = repositoryRegistry ?? throw new ArgumentNullException(nameof(repositoryRegistry));

            Context.ChangeTracker.LazyLoadingEnabled = false;
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbContext Context { get; private set; }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity
        {
            return repositoryRegistry.GetRepository<TEntity>();
        }

        public void Commit()
        {
            Context.SaveChanges();
            Context.ChangeTracker.Clear();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await Context.SaveChangesAsync(cancellationToken);
            Context.ChangeTracker.Clear();
        }

        bool disposed;

        public void Dispose()
        {
            if (disposed)
                return;

            disposed = true;

            Context.Dispose();
        }
    }
}
