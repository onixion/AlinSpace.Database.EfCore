using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlinSpace.Database.EfCore
{
    /// <summary>
    /// Represents the repository interface.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity.</typeparam>
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Gets the DB set.
        /// </summary>
        DbSet<TEntity> DbSet { get; }

        /// <summary>
        /// Start new query.
        /// </summary>
        /// <returns>Queryable.</returns>
        IQueryable<TEntity> NewQuery();

        #region Crud

        #region Get

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <param name="id">ID of the entity.</param>
        /// <param name="includer">Optional includer to apply.</param>
        /// <returns>Entity.</returns>
        TEntity Get(long id, QueryOperation<TEntity> includer = null);

        /// <summary>
        /// Gets the entity or default.
        /// </summary>
        /// <param name="id">ID of the entity.</param>
        /// <param name="includer">Optional includer to apply.</param>
        /// <returns>Entity or default.</returns>
        TEntity GetOrDefault(long id, QueryOperation<TEntity> includer = null);

        #endregion

        #region Get Async

        /// <summary>
        /// Gets the entity asynchronously.
        /// </summary>
        /// <param name="id">ID of the entity.</param>
        /// <param name="includer">Optional includer to apply.</param>
        /// <returns>Entity.</returns>
        Task<TEntity> GetAsync(long id, QueryOperation<TEntity> includer = null);

        /// <summary>
        /// Gets the entity or default asynchronously.
        /// </summary>
        /// <param name="id">ID of the entity.</param>
        /// <param name="includer">Optional includer to apply.</param>
        /// <returns>Entity or default.</returns>
        Task<TEntity> GetOrDefaultAsync(long id, QueryOperation<TEntity> includer = null);

        #endregion

        /// <summary>
        /// Creates the entity.
        /// </summary>
        /// <param name="entity">Entity to create.</param>
        /// <remarks>
        /// Primary key of the entity will be set when committing the changes.
        /// </remarks>
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
        /// <remarks>
        /// Primary key of the entity will be set when committing the changes.
        /// If the primary key is not set, then a new entity will be created.
        /// If the primary key is set, then only the changed fields of the entity will be updated.
        /// </remarks>
        void CreateOrUpdate(TEntity entity);

        /// <summary>
        /// Creates or updates the entity asynchronously.
        /// </summary>
        /// <param name="entity">Entity to create or update.</param>
        /// <remarks>
        /// If the primary key is not set, then a new entity will be created.
        /// If the primary key is set, then only the changed fields of the entity will be updated.
        /// </remarks>
        Task CreateOrUpdateAsync(TEntity entity);

        /// <summary>
        /// Creates or updates the entity asynchronously.
        /// </summary>
        /// <param name="entity">Entity to create or update.</param>
        /// <param name="update">Update action.</param>
        /// <remarks>
        /// If the primary key is not set, then a new entity will be created.
        /// If the primary key is set, then only the changed fields of the entity will be updated.
        /// <paramref name="update"/> action will be called on create and update.
        /// </remarks>
        Task CreateOrUpdateAsync(TEntity entity, Action<TEntity> update);

        /// <summary>
        /// Update the entity.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        /// <remarks>
        /// Primary key of the entity can't be null.
        /// All fields will be updated.
        /// </remarks>
        void Update(TEntity entity);

        /// <summary>
        /// Update the entity.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        /// <param name="update">Update action.</param>
        /// <remarks>
        /// Primary key of the entity can't be null.
        /// Only changed fields will be updated.
        /// </remarks>
        void Update(TEntity entity, Action<TEntity> update);

        /// <summary>
        /// Delete the entity.
        /// </summary>
        /// <param name="entity">Entity to delete.</param>
        /// <param name="softDelete">Flag indicates soft deletion.</param>
        void Delete(TEntity entity, bool softDelete = false);

        #endregion

        #region Collection

        /// <summary>
        /// Finds many entities.
        /// </summary>
        /// <param name="filter">Optional filter to apply.</param>
        /// <param name="pager">Optional pager to apply.</param>
        /// <param name="includer">Optional includer to apply.</param>
        /// <returns>Entities.</returns>
        IEnumerable<TEntity> FindMany(QueryOperation<TEntity> filter = null, Pager pager = null, QueryOperation<TEntity> includer = null);

        /// <summary>
        /// Finds many entities.
        /// </summary>
        /// <param name="filter">Optional filter to apply.</param>
        /// <param name="pager">Optional pager to apply.</param>
        /// <param name="includer">Optional includer to apply.</param>
        /// <returns>Entities.</returns>
        Task<IEnumerable<TEntity>> FindManyAsync(QueryOperation<TEntity> filter = null, Pager pager = null, QueryOperation<TEntity> includer = null);

        #endregion

        #region Find

        /// <summary>
        /// Finds the entity.
        /// </summary>
        /// <param name="filter">Filter to apply.</param>
        /// <param name="includer">Includer to apply.</param>
        /// <returns>Found entity.</returns>
        TEntity FindFirst(QueryOperation<TEntity> filter = null, QueryOperation<TEntity> includer = null);

        /// <summary>
        /// Finds the entity or default.
        /// </summary>
        /// <param name="filter">Filter to apply.</param>
        /// <param name="includer">Includer to apply.</param>
        /// <returns>Found entity or default.</returns>
        TEntity FindFirstOrDefault(QueryOperation<TEntity> filter = null, QueryOperation<TEntity> includer = null);

        #endregion

        #region Find Async

        /// <summary>
        /// Finds the entity asynchronously.
        /// </summary>
        /// <param name="filter">Filter to apply.</param>
        /// <param name="includer">Includer to apply.</param>
        /// <returns>Found entity.</returns>
        Task<TEntity> FindFirstAsync(QueryOperation<TEntity> filter = null, QueryOperation<TEntity> includer = null);

        /// <summary>
        /// Finds the entity or default asynchronously.
        /// </summary>
        /// <param name="filter">Filter to apply.</param>
        /// <param name="includer">Includer to apply.</param>
        /// <returns>Found entity or default.</returns>
        Task<TEntity> FindFirstOrDefaultAsync(QueryOperation<TEntity> filter = null, QueryOperation<TEntity> includer = null);

        #endregion
    }
}
