using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AlinSpace.Database.Ef
{
    /// <summary>
    /// Represents the default implementation for <see cref="ICrudRepository{TEntity, TPrimaryKey}"/>.
    /// </summary>
    /// <typeparam name="TEntityWithId">Type of the entity with ID.</typeparam>
    /// <typeparam name="TPrimaryKey">Type of the primary key.</typeparam>
    public class CrudRepository<TEntityWithId, TPrimaryKey> : ICrudRepository<TEntityWithId, TPrimaryKey>
        where TEntityWithId : class, IEntityWithId<TPrimaryKey>
        where TPrimaryKey : struct
    {
        private readonly ITransaction transaction;
        private readonly IRepository<TEntityWithId> repository;

        /// <summary>
        /// Constructor.
        /// </summary>
        public CrudRepository(ITransaction transaction)
        {
            this.transaction = transaction;
            repository = transaction.GetRepository<TEntityWithId>();
        }

        public async Task<TPrimaryKey> AddAsync(TEntityWithId entity)
        {
            var repository = transaction.GetRepository<TEntityWithId>();

            await repository.AddAsync(entity);
            await transaction.CommitAsync();

            return entity.Id;
        }

        public async Task<TEntityWithId> GetOrDefaultAsync(
            TPrimaryKey primaryKey,
            Func<IQueryable<TEntityWithId>, IQueryable<TEntityWithId>> queryableFunc = null,
            QueryOptions options = null,
            TEntityWithId defaultValue = default)
        {
            var repository = transaction.GetRepository<TEntityWithId>();

            var query = repository
                .NewQuery(options)
                .Where(x => x.Id.Equals(primaryKey));

            query = queryableFunc?.Invoke(query) ?? query;

            return (await query.FirstOrDefaultAsync()) ?? defaultValue;
        }

        public async Task<TEntityWithId> GetAsync(
            TPrimaryKey primaryKey,
            Func<IQueryable<TEntityWithId>, IQueryable<TEntityWithId>> queryableFunc = null,
            QueryOptions options = null)
        {
            var entity = await GetOrDefaultAsync(primaryKey, queryableFunc, options);

            if (entity == null)
                throw new EntityNotFoundException(primaryKey);

            return entity;
        }

        public async Task UpdateAsync(
            TEntityWithId entity,
            bool commit = false)
        {
            var repository = transaction.GetRepository<TEntityWithId>();

            repository.Update(entity);

            if (commit)
            {
                await transaction.CommitAsync();
            }
        }

        public async Task UpdateAsync(
            TPrimaryKey primaryKey,
            Action<TEntityWithId> updateAction,
            Func<IQueryable<TEntityWithId>, IQueryable<TEntityWithId>> queryableFunc = null,
            bool commit = false)
        {
            var repository = transaction.GetRepository<TEntityWithId>();

            var entity = await GetAsync(
                primaryKey,
                queryableFunc: queryableFunc,
                options: QueryOptions.WithTracking);

            updateAction(entity);

            await UpdateAsync(entity, commit);
        }

        public async Task DeleteAsync(
            TEntityWithId entity,
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
            Func<IQueryable<TEntityWithId>, IQueryable<TEntityWithId>> queryableFunc = null,
            bool commit = false,
            bool hard = false)
        {
            var query = repository
                .NewQuery(QueryOptions.WithTracking)
                .Where(x => x.Id.Equals(primaryKey));

            query = queryableFunc?.Invoke(query) ?? query;

            var entity = await query.FirstOrDefaultAsync();

            if (entity == null)
                return;

            await DeleteAsync(entity, commit, hard);
        }
    }
}
