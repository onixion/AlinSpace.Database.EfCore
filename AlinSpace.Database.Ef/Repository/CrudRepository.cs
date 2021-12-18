using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AlinSpace.Database.Ef
{
    /// <summary>
    /// Default implementation for <see cref="ICrudRepository{TEntity, TPrimaryKey}"/>.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TPrimaryKey">Type of primary key.</typeparam>
    public class CrudRepository<TEntity, TPrimaryKey> : ICrudRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TPrimaryKey : struct
    {
        private readonly ITransaction transaction;
        private readonly IRepository<TEntity> repository;

        /// <summary>
        /// Constructor.
        /// </summary>
        public CrudRepository(ITransaction transaction)
        {
            this.transaction = transaction;
            repository = transaction.GetRepository<TEntity>();
        }

        public async Task<TPrimaryKey> AddAsync(TEntity entity)
        {
            var repository = transaction.GetRepository<TEntity>();

            await repository.AddAsync(entity);
            await transaction.CommitAsync();

            return entity.Id;
        }

        public async Task<TEntity> GetOrDefaultAsync(
            TPrimaryKey primaryKey,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includeConfigurator = null,
            QueryOptions options = null)
        {
            var repository = transaction.GetRepository<TEntity>();

            var query = repository
                .NewQuery(options)
                .Where(x => x.Id.Equals(primaryKey));

            query = includeConfigurator?.Invoke(query) ?? query;

            return await query.FirstOrDefaultAsync();
        }

        // todo maybe make this an extension?
        public async Task<TEntity> GetAsync(
            TPrimaryKey primaryKey,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includeConigurator = null,
            QueryOptions options = null)
        {
            var entity = await GetOrDefaultAsync(primaryKey, includeConigurator, options);

            if (entity == null)
                throw new EntityNotFoundException(primaryKey);

            return entity;
        }

        public async Task UpdateAsync(
            TEntity entity,
            bool commit = false)
        {
            var repository = transaction.GetRepository<TEntity>();

            repository.Update(entity);

            if (commit)
            {
                await transaction.CommitAsync();
            }
        }

        public async Task UpdateAsync(
            TPrimaryKey primaryKey,
            Action<TEntity> updateAction,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includeConigurator = null,
            bool commit = false)
        {
            var repository = transaction.GetRepository<TEntity>();

            var entity = await GetAsync(
                primaryKey,
                includeConigurator: includeConigurator,
                options: QueryOptions.WithTracking);

            updateAction(entity);

            await UpdateAsync(entity, commit);
        }

        public async Task DeleteAsync(
            TEntity entity,
            bool commit = false,
            bool hard = false)
        {
            if (hard)
            {
                repository.Delete(entity);
            }
            else
            {
                entity.IsDeleted = true;
                await UpdateAsync(entity, commit: false);
            }

            if (commit)
            {
                await transaction.CommitAsync();
            }
        }

        public async Task DeleteAsync(
            TPrimaryKey primaryKey,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null,
            bool commit = false,
            bool hard = false)
        {
            var query = repository
                .NewQuery(QueryOptions.WithTracking)
                .Where(x => x.Id.Equals(primaryKey));

            if (include != null)
                query = include(query);

            var entity = await query.FirstOrDefaultAsync();

            if (entity == null)
                return;

            await DeleteAsync(entity, commit, hard);
        }
    }
}
