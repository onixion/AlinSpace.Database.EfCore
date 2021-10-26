using System.Linq;
using System.Threading.Tasks;

namespace AlinSpace.Database
{
    /// <summary>
    /// Represents the repository interface.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity.</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Returns a new query.
        /// </summary>
        /// <returns>New query.</returns>
        IQueryable<TEntity> NewQuery();

        /// <summary>
        /// Adds entity.
        /// </summary>
        /// <param name="entity">Entity to add.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Adds entity asynchronously.
        /// </summary>
        /// <param name="entity">Entity to add.</param>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Add or update entity.
        /// </summary>
        /// <param name="entity">Entity to add or update.</param>
        void AddOrUpdate(TEntity entity);

        /// <summary>
        /// Add or update entity asynchronously.
        /// </summary>
        /// <param name="entity">Entity to add or update.</param>
        Task AddOrUpdateAsync(TEntity entity);

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Delete entity.
        /// </summary>
        /// <param name="entity">Entity to delete.</param>
        void Delete(TEntity entity);
    }
}
