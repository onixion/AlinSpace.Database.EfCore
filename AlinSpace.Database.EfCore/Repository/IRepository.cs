using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlinSpace.Database.EfCore
{
    /// <summary>
    /// Represents the repository interface.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity.</typeparam>
    public interface IRepository<TEntity>where TEntity : class, IEntity
    {
        /// <summary>
        /// Gets the DB set.
        /// </summary>
        DbSet<TEntity> DbSet { get; }

        /// <summary>
        /// Start new query.
        /// </summary>
        /// <param name="tracking">Tracking.</param>
        /// <returns>Queryable.</returns>
        IQueryable<TEntity> NewQuery(bool tracking = false);

        #region Crud

        #region Get

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <param name="id">ID of the entity.</param>
        /// <param name="includer">Optional includer to apply.</param>
        /// <returns>Entity.</returns>
        TEntity Get(long id, Includer<TEntity> includer = null);

        /// <summary>
        /// Gets the entity or default.
        /// </summary>
        /// <param name="id">ID of the entity.</param>
        /// <param name="includer">Optional includer to apply.</param>
        /// <returns>Entity or default.</returns>
        TEntity GetOrDefault(long id, Includer<TEntity> includer = null);

        #endregion

        #region Get Async

        /// <summary>
        /// Gets the entity asynchronously.
        /// </summary>
        /// <param name="id">ID of the entity.</param>
        /// <param name="includer">Optional includer to apply.</param>
        /// <returns>Entity.</returns>
        Task<TEntity> GetAsync(long id, Includer<TEntity> includer = null);

        /// <summary>
        /// Gets the entity or default asynchronously.
        /// </summary>
        /// <param name="id">ID of the entity.</param>
        /// <param name="includer">Optional includer to apply.</param>
        /// <returns>Entity or default.</returns>
        Task<TEntity> GetOrDefaultAsync(long id, Includer<TEntity> includer = null);

        #endregion

        /// <summary>
        /// Creates the entity.
        /// </summary>
        /// <param name="entity">Entity to create.</param>
        void Create(TEntity entity);

        /// <summary>
        /// Creates the entity asynchronously.
        /// </summary>
        /// <param name="entity">Entity to create.</param>
        Task CreateAsync(TEntity entity);

        /// <summary>
        /// Creates or updates the entity.
        /// </summary>
        /// <param name="entity">Entity to create or update.</param>
        void CreateOrUpdate(TEntity entity);

        /// <summary>
        /// Creates or updates the entity asynchronously.
        /// </summary>
        /// <param name="entity">Entity to create or update.</param>
        Task CreateOrUpdateAsync(TEntity entity);

        /// <summary>
        /// Update the entity.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Update the entity asynchronously.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Delete the entity.
        /// </summary>
        /// <param name="entity">Entity to delete.</param>
        /// <param name="softDelete">Flag indicates soft deletion.</param>
        void Delete(TEntity entity, bool softDelete = false);

        /// <summary>
        /// Delete the entity.
        /// </summary>
        /// <param name="entity">Entity to delete.</param>
        /// <param name="softDelete">Flag indicates soft deletion.</param>
        Task DeleteAsync(TEntity entity, bool softDelete = false);

        #endregion

        #region Collection

        /// <summary>
        /// Finds many entities.
        /// </summary>
        /// <param name="filter">Optional filter to apply.</param>
        /// <param name="pager">Optional pager to apply.</param>
        /// <param name="includer">Optional includer to apply.</param>
        /// <returns>Entities.</returns>
        IEnumerable<TEntity> FindMany(Filter<TEntity> filter = null, Pager pager = null, Includer<TEntity> includer = null);

        /// <summary>
        /// Finds many entities.
        /// </summary>
        /// <param name="filter">Optional filter to apply.</param>
        /// <param name="pager">Optional pager to apply.</param>
        /// <param name="includer">Optional includer to apply.</param>
        /// <returns>Entities.</returns>
        Task<IEnumerable<TEntity>> FindManyAsync(Filter<TEntity> filter = null, Pager pager = null, Includer<TEntity> includer = null);

        #endregion

        #region Find

        /// <summary>
        /// Finds the entity.
        /// </summary>
        /// <param name="filter">Filter to apply.</param>
        /// <param name="includer">Includer to apply.</param>
        /// <returns>Found entity.</returns>
        TEntity FindFirst(Filter<TEntity> filter = null, Includer<TEntity> includer = null);

        /// <summary>
        /// Finds the entity or default.
        /// </summary>
        /// <param name="filter">Filter to apply.</param>
        /// <param name="includer">Includer to apply.</param>
        /// <returns>Found entity or default.</returns>
        TEntity FindFirstOrDefault(Filter<TEntity> filter = null, Includer<TEntity> includer = null);

        #endregion

        #region Find Async

        /// <summary>
        /// Finds the entity asynchronously.
        /// </summary>
        /// <param name="filter">Filter to apply.</param>
        /// <param name="includer">Includer to apply.</param>
        /// <returns>Found entity.</returns>
        Task<TEntity> FindFirstAsync(Filter<TEntity> filter = null, Includer<TEntity> includer = null);

        /// <summary>
        /// Finds the entity or default asynchronously.
        /// </summary>
        /// <param name="filter">Filter to apply.</param>
        /// <param name="includer">Includer to apply.</param>
        /// <returns>Found entity or default.</returns>
        Task<TEntity> FindFirstOrDefaultAsync(Filter<TEntity> filter = null, Includer<TEntity> includer = null);

        #endregion
    }
}
