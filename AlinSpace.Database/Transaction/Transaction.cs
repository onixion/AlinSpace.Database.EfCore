using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AlinSpace.Database
{
    class Transaction : ITransaction
    {
        private readonly DbContext dbContext;
        private readonly RepositoryRegistry repositoryRegistry;
        
        public Transaction(DbContext dbContext, RepositoryRegistry repositoryRegistry)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.repositoryRegistry = repositoryRegistry ?? throw new ArgumentNullException(nameof(repositoryRegistry));
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : IEntity
        {
            return repositoryRegistry.GetRepository<TEntity>();
        }

        public void Commit()
        {
            dbContext.SaveChanges();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        bool disposed;

        public void Dispose()
        {
            if (disposed)
                return;

            disposed = true;

            dbContext.Dispose();
        }
    }
}
