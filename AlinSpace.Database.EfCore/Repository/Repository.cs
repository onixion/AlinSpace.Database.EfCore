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

        public IQueryable<TEntity> NewQuery(bool tracking = false)
        {
            if (tracking)
            {
                return DbSet.AsTracking();
            }
            else
            {
                return DbSet;
            }
        }

        #region Crud

        #region Get

        public TEntity Get(long id, Includer<TEntity> includer = null)
        {
            var query = NewQuery().Where(x => x.Id == id);

            if (includer != null)
                query = includer.IncluderOperation(query);

            return query.First();
        }

        public TEntity GetOrDefault(long id, Includer<TEntity> includer = null)
        {
            var query = NewQuery().Where(x => x.Id == id);

            if (includer != null)
                query = includer.IncluderOperation(query);

            return query.FirstOrDefault();
        }

        #endregion

        #region Get Async

        public async Task<TEntity> GetAsync(long id, Includer<TEntity> includer = null)
        {
            var query = NewQuery().Where(x => x.Id == id);

            if (includer != null)
                query = includer.IncluderOperation(query);

            return await query.FirstAsync();
        }

        public async Task<TEntity> GetOrDefaultAsync(long id, Includer<TEntity> includer = null)
        {
            var query = NewQuery().Where(x => x.Id == id);

            if (includer != null)
                query = includer.IncluderOperation(query);

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
            entity.CreationTimestamp = DateTimeOffset.UtcNow;
            await DbSet.AddAsync(entity);
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

            DbSet.Update(entity);
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

            DbSet.Update(entity);
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

            DbSet.Update(entity);
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

            DbSet.Update(entity);
            return Task.CompletedTask;
        }

        public void Delete(TEntity entity, bool softDelete = false)
        {
            if (softDelete)
            {
                entity.DeletionTimestamp = DateTimeOffset.UtcNow;
                entity.IsDeleted = true;
                DbSet.Update(entity);
            }
            else
            {
                DbSet.Remove(entity);
            }
        }

        public Task DeleteAsync(TEntity entity, bool softDelete = false)
        {
            if (softDelete)
            {
                entity.DeletionTimestamp = DateTimeOffset.UtcNow;
                entity.IsDeleted = true;
                DbSet.Update(entity);
            }
            else
            {
                DbSet.Remove(entity);
            }

            return Task.CompletedTask;
        }

        #endregion

        #region Collection

        public IEnumerable<TEntity> FindMany(Filter<TEntity> filter = null, Pager pager = null, Includer<TEntity> includer = null)
        {
            var query = NewQuery();

            if (filter != null)
                query = filter.FilterOperation(query);

            if (pager != null)
                query = pager.TakePage(query);

            if (includer != null)
                query = includer.IncluderOperation(query);

            return query.ToList();
        }

        public async Task<IEnumerable<TEntity>> FindManyAsync(Filter<TEntity> filter = null, Pager pager = null, Includer<TEntity> includer = null)
        {
            var query = NewQuery();

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
            var query = NewQuery();

            if (filter != null)
                query = filter.FilterOperation(query);

            if (includer != null)
                query = includer.IncluderOperation(query);

            return query.First();
        }

        public TEntity FindFirstOrDefault(Filter<TEntity> filter = null, Includer<TEntity> includer = null)
        {
            var query = NewQuery();

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
            var query = NewQuery();

            if (filter != null)
                query = filter.FilterOperation(query);

            if (includer != null)
                query = includer.IncluderOperation(query);

            return await query.FirstAsync();
        }

        public async Task<TEntity> FindFirstOrDefaultAsync(Filter<TEntity> filter = null, Includer<TEntity> includer = null)
        {
            var query = NewQuery();

            if (filter != null)
                query = filter.FilterOperation(query);

            if (includer != null)
                query = includer.IncluderOperation(query);

            return await query.FirstOrDefaultAsync();
        }

        #endregion
    }
}
