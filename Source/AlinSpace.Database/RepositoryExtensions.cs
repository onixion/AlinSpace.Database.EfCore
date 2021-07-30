using System.Linq;

namespace AlinSpace.Database
{
    /// <summary>
    /// Extensions for <see cref="IRepository{TEntity, TKey}"/>.
    /// </summary>
    public static class RepositoryExtensions
    {
        /// <summary>
        /// Get a specific page.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <param name="repository">Repository to retrieve the page from.</param>
        /// <param name="page">Page number.</param>
        /// <param name="pageSize">Page size.</param>
        /// <returns>Queryable of the page.</returns>
        public static IQueryable<TEntity> GetPage<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository, 
            int page, 
            int pageSize)
            where TEntity : class
        {
            if (page <= 0)
                page = 1;

            if (pageSize <= 0)
                pageSize = 1;

            return repository
                .NewQuery()
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
