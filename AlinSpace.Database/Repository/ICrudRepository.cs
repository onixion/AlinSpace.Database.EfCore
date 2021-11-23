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
    public interface ICrudRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TPrimaryKey : struct
    {
        /// <summary>
        /// Adds the entity.
        /// </summary>
        /// <param name="entity">Entity to be added.</param>
        /// <returns>Primary key of the added entity.</returns>
        Task<TPrimaryKey> AddAsync(TEntity entity);

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <param name="primaryKey">Primary key.</param>
        /// <param name="includeConfigurator">Optional include configurator.</param>
        /// <param name="options">Optional query options.</param>
        /// <returns>Entity.</returns>
        Task<TEntity> GetAsync(
            TPrimaryKey primaryKey,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includeConfigurator = null,
            QueryOptions options = null);

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="entity">Entity to be updated.</param>
        /// <param name="commit">Commit changes.</param>
        Task UpdateAsync(
            TEntity entity,
            bool commit = false);

        /// <summary>
        /// Fetch and update entity.
        /// </summary>
        /// <param name="primaryKey">Primary key of the entity to updated.</param>
        /// <param name="updateAction">Update action.</param>
        /// <param name="includeConigurator">Optional include conigurator.</param>
        /// <param name="commit">Commit changes.</param>
        Task UpdateAsync(
            TPrimaryKey primaryKey,
            Action<TEntity> updateAction,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includeConigurator = null,
            bool commit = false);

        /// <summary>
        /// Delete the entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        /// <param name="commit">Commit.</param>
        /// <param name="soft">Perform soft delete.</param>
        Task DeleteAsync(
            TEntity entity,
            bool commit = false,
            bool soft = false);

        /// <summary>
        /// Delete entity by given primary key.
        /// </summary>
        /// <param name="primaryKey">Primary key of entity to delete.</param>
        /// <param name="commit">Commit.</param>
        /// <param name="soft">Perform soft delete.</param>
        Task DeleteAsync(
            TPrimaryKey primaryKey,
            bool commit = false,
            bool soft = false);
    }
}
