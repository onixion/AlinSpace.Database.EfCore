using System;
using System.Linq;
using System.Threading.Tasks;

namespace AlinSpace.Database
{
    /// <summary>
    /// Represents the CRUD repository.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity.</typeparam>
    /// <typeparam name="TPrimaryKey">Type of the primary key.</typeparam>
    public interface ICrudRepository<TEntityWithId, TPrimaryKey>
        where TEntityWithId : class, IEntityWithId<TPrimaryKey>
        where TPrimaryKey : struct
    {
        /// <summary>
        /// Adds the entity.
        /// </summary>
        /// <param name="entity">Entity to be added.</param>
        /// <returns>Primary key of the added entity.</returns>
        Task<TPrimaryKey> AddAsync(TEntityWithId entity);

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <param name="primaryKey">Primary key.</param>
        /// <param name="queryableFunc">Optional queryable func.</param>
        /// <param name="options">Optional query options.</param>
        /// <returns>Entity.</returns>
        Task<TEntityWithId> GetAsync(
            TPrimaryKey primaryKey,
            Func<IQueryable<TEntityWithId>, IQueryable<TEntityWithId>> queryableFunc = null,
            QueryOptions options = null);

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="entity">Entity to be updated.</param>
        /// <param name="commit">Commit changes.</param>
        Task UpdateAsync(
            TEntityWithId entity,
            bool commit = false);

        /// <summary>
        /// Fetch and update entity.
        /// </summary>
        /// <param name="primaryKey">Primary key of the entity to updated.</param>
        /// <param name="updateAction">Update action.</param>
        /// <param name="queryableFunc">Optional queryable func.</param>
        /// <param name="commit">Commit changes.</param>
        Task UpdateAsync(
            TPrimaryKey primaryKey,
            Action<TEntityWithId> updateAction,
            Func<IQueryable<TEntityWithId>, IQueryable<TEntityWithId>> queryableFunc = null,
            bool commit = false);

        /// <summary>
        /// Delete the entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        /// <param name="commit">Commit.</param>
        /// <param name="hard">Perform hard delete.</param>
        Task DeleteAsync(
            TEntityWithId entity,
            bool commit = false,
            bool hard = false);

        /// <summary>
        /// Delete entity by given primary key.
        /// </summary>
        /// <param name="primaryKey">Primary key of entity to delete.</param>
        /// <param name="queryableFunc">Optional queryable func.</param>
        /// <param name="commit">Commit.</param>
        /// <param name="hard">Perform hard delete.</param>
        Task DeleteAsync(
            TPrimaryKey primaryKey,
            Func<IQueryable<TEntityWithId>, IQueryable<TEntityWithId>> queryableFunc = null,
            bool commit = false,
            bool hard = false);
    }
}
