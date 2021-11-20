using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AlinSpace.Database.Ef
{
    /// <summary>
    /// Represents an implementation of <see cref="IRepository{TEntity}"/> for EF.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity.</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbContext">Database context.</param>
        /// <param name="dbSet">Database set.</param>
        public Repository(DbContext dbContext, DbSet<TEntity> dbSet)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.dbSet = dbSet ?? throw new ArgumentNullException(nameof(dbSet));
        }

        /// <summary>
        /// Returns a new query.
        /// </summary>
        /// <param name="options">Optional query options.</param>
        /// <returns>New query.</returns>
        public IQueryable<TEntity> NewQuery(QueryOptions options = null)
        {
            options ??= new QueryOptions();

            if (options.Tracking)
            {
                return dbSet.AsTracking();
            }
            else
            {
                return dbSet.AsQueryable();
            }
        }

        /// <summary>
        /// Adds entity.
        /// </summary>
        /// <param name="entity">Entity to add.</param>
        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// Adds entity asynchronously.
        /// </summary>
        /// <param name="entity">Entity to add.</param>
        public async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        /// <summary>
        /// Add or update entity.
        /// </summary>
        /// <param name="entity">Entity to add or update.</param>
        public void AddOrUpdate(TEntity entity)
        {
            var entry = dbContext.Entry(entity);

            switch (entry.State)
            {
                case EntityState.Detached:
                case EntityState.Added:
                    dbContext.Add(entity);
                    break;

                case EntityState.Modified:
                    dbContext.Update(entity);
                    break;

                case EntityState.Unchanged:
                    break;

                default:
                    throw new Exception($"Unsupported entry state {entry.State}.");
            }
        }

        /// <summary>
        /// Add or update entity asynchronously.
        /// </summary>
        /// <param name="entity">Entity to add or update.</param>
        public async Task AddOrUpdateAsync(TEntity entity)
        {
            var entry = dbContext.Entry(entity);

            switch (entry.State)
            {
                case EntityState.Detached:
                case EntityState.Added:
                    await dbContext.AddAsync(entity);
                    break;

                case EntityState.Modified:
                    dbContext.Update(entity);
                    break;

                case EntityState.Unchanged:
                    break;

                default:
                    throw new Exception($"Unsupported entry state {entry.State}.");
            }
        }

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        } 

        /// <summary>
        /// Delete entity.
        /// </summary>
        /// <param name="entity">Entity to delete.</param>
        public void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
        }
    }
}
