using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlinSpace.Database
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        public Repository(DbContext dbContext, DbSet<TEntity> dbSet)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.dbSet = dbSet ?? throw new ArgumentNullException(nameof(dbSet));
        }

        public bool Tracking { get; set; }

        public IQueryable<TEntity> Query
        {
            get
            {
                if (Tracking)
                {
                    return dbSet.AsTracking();
                }
                else
                {
                    return dbSet.AsQueryable();
                }
            }
        }

        #region Crud

        #region Get

        public TEntity Get(long id, Includer<TEntity> includer = null)
        {
            var query = Query.Where(x => x.Id == id);

            if (includer != null)
                query = includer.IncluderOperation(query);

            return query.First();
        }

        public TEntity GetOrDefault(long id, Includer<TEntity> includer = null)
        {
            var query = Query.Where(x => x.Id == id);

            if (includer != null)
                query = includer.IncluderOperation(query);

            return query.FirstOrDefault();
        }

        #endregion

        #region Get Async

        public async Task<TEntity> GetAsync(long id, Includer<TEntity> includer = null)
        {
            var query = Query.Where(x => x.Id == id);

            if (includer != null)
                query = includer.IncluderOperation(query);

            return await query.FirstAsync();
        }

        public async Task<TEntity> GetOrDefaultAsync(long id, Includer<TEntity> includer = null)
        {
            var query = Query.Where(x => x.Id == id);

            if (includer != null)
                query = includer.IncluderOperation(query);

            return await query.FirstOrDefaultAsync();
        }

        #endregion

        public void Create(TEntity entity)
        {
            entity.CreationTimestamp = DateTimeOffset.UtcNow;
            dbSet.Add(entity);
        }

        public async Task CreateAsync(TEntity entity)
        {
            entity.CreationTimestamp = DateTimeOffset.UtcNow;
            await dbSet.AddAsync(entity);
        }

        public void CreateOrUpdate(TEntity entity)
        {
            if (entity.Id == default)
            {
                entity.CreationTimestamp = DateTimeOffset.UtcNow;
            }
            else
            {
                entity.ModificationTimestamp = DateTimeOffset.UtcNow;
            }

            dbSet.Update(entity);
        }

        public Task CreateOrUpdateAsync(TEntity entity)
        {
            if (entity.Id == default)
            {
                entity.CreationTimestamp = DateTimeOffset.UtcNow;
            }
            else
            {
                entity.ModificationTimestamp = DateTimeOffset.UtcNow;
            }

            dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public void Update(TEntity entity)
        {
            if (entity.Id == default)
            {
                entity.CreationTimestamp = DateTimeOffset.UtcNow;
            }
            else
            {
                entity.ModificationTimestamp = DateTimeOffset.UtcNow;
            }

            dbSet.Update(entity);
        }

        public Task UpdateAsync(TEntity entity)
        {
            if (entity.Id == default)
            {
                entity.CreationTimestamp = DateTimeOffset.UtcNow;
            }
            else
            {
                entity.ModificationTimestamp = DateTimeOffset.UtcNow;
            }

            dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public void Delete(TEntity entity, bool softDelete = false)
        {
            if (softDelete)
            {
                entity.DeletionTimestamp = DateTimeOffset.UtcNow;
                entity.IsDeleted = true;
                dbSet.Update(entity);
            }
            else
            {
                dbSet.Remove(entity);
            }
        }

        public Task DeleteAsync(TEntity entity, bool softDelete = false)
        {
            if (softDelete)
            {
                entity.DeletionTimestamp = DateTimeOffset.UtcNow;
                entity.IsDeleted = true;
                dbSet.Update(entity);
            }
            else
            {
                dbSet.Remove(entity);
            }

            return Task.CompletedTask;
        }

        #endregion

        #region Collection

        public IEnumerable<TEntity> GetMany(Filter<TEntity> filter = null, Pager pager = null, Includer<TEntity> includer = null)
        {
            var query = Query;

            if (filter != null)
                query = filter.FilterOperation(query);

            if (pager != null)
                query = pager.TakePage(query);

            if (includer != null)
                query = includer.IncluderOperation(query);

            return query.ToList();
        }

        public async Task<IEnumerable<TEntity>> GetManyAsync(Filter<TEntity> filter = null, Pager pager = null, Includer<TEntity> includer = null)
        {
            var query = Query;

            if (filter != null)
                query = filter.FilterOperation(query);

            if (pager != null)
                query = pager.TakePage(query);

            if (includer != null)
                query = includer.IncluderOperation(query);

            return await query.ToListAsync();
        }

        #endregion

        #region Find

        public TEntity FindFirst(Filter<TEntity> filter = null, Includer<TEntity> includer = null)
        {
            var query = Query;

            if (filter != null)
                query = filter.FilterOperation(query);

            if (includer != null)
                query = includer.IncluderOperation(query);

            return query.First();
        }

        public TEntity FindFirstOrDefault(Filter<TEntity> filter = null, Includer<TEntity> includer = null)
        {
            var query = Query;

            if (filter != null)
                query = filter.FilterOperation(query);

            if (includer != null)
                query = includer.IncluderOperation(query);

            return query.FirstOrDefault();
        }

        #endregion

        #region Find Async

        public async Task<TEntity> FindFirstAsync(Filter<TEntity> filter = null, Includer<TEntity> includer = null)
        {
            var query = Query;

            if (filter != null)
                query = filter.FilterOperation(query);

            if (includer != null)
                query = includer.IncluderOperation(query);

            return await query.FirstAsync();
        }

        public async Task<TEntity> FindFirstOrDefaultAsync(Filter<TEntity> filter = null, Includer<TEntity> includer = null)
        {
            var query = Query;

            if (filter != null)
                query = filter.FilterOperation(query);

            if (includer != null)
                query = includer.IncluderOperation(query);

            return await query.FirstOrDefaultAsync();
        }

        #endregion
    }
}
