using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlinSpace.Database.EfCore
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbContext dbContext;

        public Repository(DbContext dbContext, DbSet<TEntity> dbSet)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbSet = dbSet ?? throw new ArgumentNullException(nameof(dbSet));
        }

        public DbSet<TEntity> DbSet { get; private set; }

        public IQueryable<TEntity> NewQuery()
        {
            return DbSet.AsNoTracking();
        }

        #region Crud

        #region Get

        public TEntity Get(long id, QueryOperation<TEntity> includer = null)
        {
            var query = NewQuery().Where(x => x.Id == id);

            query = includer?.Invoke(query) ?? query;

            return query.First();
        }

        public TEntity GetOrDefault(long id, QueryOperation<TEntity> includer = null)
        {
            var query = NewQuery().Where(x => x.Id == id);

            query = includer?.Invoke(query) ?? query;

            return query.FirstOrDefault();
        }

        #endregion

        #region Get Async

        public async Task<TEntity> GetAsync(long id, QueryOperation<TEntity> includer = null)
        {
            var query = NewQuery().Where(x => x.Id == id);

            query = includer?.Invoke(query) ?? query;

            return await query.FirstAsync();
        }

        public async Task<TEntity> GetOrDefaultAsync(long id, QueryOperation<TEntity> includer = null)
        {
            var query = NewQuery().Where(x => x.Id == id);

            query = includer?.Invoke(query) ?? query;

            return await query.FirstOrDefaultAsync();
        }

        #endregion

        public void Create(TEntity entity)
        {
            entity.CreationTimestamp = DateTimeOffset.UtcNow;
            DbSet.Add(entity);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            entity.CreationTimestamp = DateTimeOffset.UtcNow;
        }

        public void CreateOrUpdate(TEntity entity)
        {
            if (entity.Id == default)
            {
                dbContext.Entry(entity).State = EntityState.Added;
                entity.CreationTimestamp = DateTimeOffset.UtcNow;
            }
            else
            {
                dbContext.Entry(entity).State = EntityState.Modified;
                entity.ModificationTimestamp = DateTimeOffset.UtcNow;
            }
        }

        public async Task CreateOrUpdateAsync(TEntity entity)
        {
            if (entity.Id == default)
            {
                await DbSet.AddAsync(entity);
                entity.CreationTimestamp = DateTimeOffset.UtcNow;
            }
            else
            {
                dbContext.Entry(entity).State = EntityState.Modified;
                entity.ModificationTimestamp = DateTimeOffset.UtcNow;
            }
        }

        public async Task CreateOrUpdateAsync(TEntity entity, Action<TEntity> update)
        {
            if (entity.Id == default)
            {
                await DbSet.AddAsync(entity);
            }
            else
            {
                dbContext.Entry(entity).State = EntityState.Unchanged;
            }

            update(entity);
            entity.CreationTimestamp = DateTimeOffset.UtcNow;
        }

        public void Update(TEntity entity)
        {
            if (entity.Id == default)
                throw new PrimaryKeyException($"Primary key of entity ({typeof(TEntity).FullName}) is not set.");

            dbContext.Entry(entity).State = EntityState.Modified;
            entity.ModificationTimestamp = DateTimeOffset.UtcNow;
        }

        public void Update(TEntity entity, Action<TEntity> update)
        {
            if (entity.Id == default)
                throw new PrimaryKeyException();

            dbContext.Entry(entity).State = EntityState.Unchanged;
            update(entity);

            entity.ModificationTimestamp = DateTimeOffset.UtcNow;
        }

        public void Delete(TEntity entity, bool softDelete = false)
        {
            if (entity.Id == default)
                throw new PrimaryKeyException($"Primary key of entity ({typeof(TEntity).FullName}) is not set.");

            if (softDelete)
            {
                dbContext.Entry(entity).State = EntityState.Modified;
                
                entity.DeletionTimestamp = DateTimeOffset.UtcNow;
                entity.IsDeleted = true;
            }
            else
            {
                //dbContext.Remove(entity);
                dbContext.Entry(entity).State = EntityState.Deleted;
            }
        }

        #endregion

        #region Collection

        public IEnumerable<TEntity> FindMany(QueryOperation<TEntity> filter = null, Pager pager = null, QueryOperation<TEntity> includer = null)
        {
            var query = NewQuery();

            query = filter?.Invoke(query) ?? query;
            
            query = pager?.TakePage(query) ?? query;
            
            query = includer?.Invoke(query) ?? query;

            return query.ToList();
        }

        public async Task<IEnumerable<TEntity>> FindManyAsync(QueryOperation<TEntity> filter = null, Pager pager = null, QueryOperation<TEntity> includer = null)
        {
            var query = NewQuery();

            query = filter?.Invoke(query) ?? query;

            query = pager?.TakePage(query) ?? query;

            query = includer?.Invoke(query) ?? query;

            return await query.ToListAsync();
        }

        #endregion

        #region Find

        public TEntity FindFirst(QueryOperation<TEntity> filter = null, QueryOperation<TEntity> includer = null)
        {
            var query = NewQuery();

            query = filter?.Invoke(query) ?? query;

            query = includer?.Invoke(query) ?? query;

            return query.First();
        }

        public TEntity FindFirstOrDefault(QueryOperation<TEntity> filter = null, QueryOperation<TEntity> includer = null)
        {
            var query = NewQuery();

            query = filter?.Invoke(query) ?? query;

            query = includer?.Invoke(query) ?? query;

            return query.FirstOrDefault();
        }

        #endregion

        #region Find Async

        public async Task<TEntity> FindFirstAsync(QueryOperation<TEntity> filter = null, QueryOperation<TEntity> includer = null)
        {
            var query = NewQuery();

            query = filter?.Invoke(query) ?? query;

            query = includer?.Invoke(query) ?? query;

            return await query.FirstAsync();
        }

        public async Task<TEntity> FindFirstOrDefaultAsync(QueryOperation<TEntity> filter = null, QueryOperation<TEntity> includer = null)
        {
            var query = NewQuery();

            query = filter?.Invoke(query) ?? query;

            query = includer?.Invoke(query) ?? query;

            return await query.FirstOrDefaultAsync();
        }

        #endregion
    }
}
