using System;
using System.Collections.Generic;
using System.Linq;

namespace AlinSpace.Database
{
    /// <summary>
    /// Extensions for <see cref="IRepository{TModel, TKey}"/>.
    /// </summary>
    public static class RepositoryExtensions
    {
        /// <summary>
        /// Get a specific page.
        /// </summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <param name="repository">Repository to retrieve the page from.</param>
        /// <param name="page">Page number.</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="func">Queryable func.</param>
        /// <returns>Enumerable of items in the page.</returns>
        public static IEnumerable<TModel> GetPage<TModel, TKey>(
            this IRepository<TModel, TKey> repository, 
            int page, 
            int pageSize = 20,
            Func<IQueryable<TModel>, IQueryable<TModel>> func = null)
            where TModel : class
        {
            return repository.Get(
                skip: page * pageSize,
                take: pageSize,
                func: func);
        }
    }
}
