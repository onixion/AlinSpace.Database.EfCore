using System;

namespace AlinSpace.Database.EfCore
{
    /// <summary>
    /// Represents the filter.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    public class Filter<TEntity>
    {
        /// <summary>
        /// Gets the query operation.
        /// </summary>
        public QueryOperation<TEntity> FilterOperation { get; private set; }

        /// <summary>
        /// Constructs with filter operation.
        /// </summary>
        /// <param name="filterOperation">Filter operation.</param>
        /// <returns>Filter.</returns>
        public static Filter<TEntity> With(QueryOperation<TEntity> filterOperation)
        {
            if (filterOperation == null)
                throw new ArgumentNullException(nameof(filterOperation));

            return new Filter<TEntity>
            {
                FilterOperation = filterOperation
            };
        }
    }

    /// <summary>
    /// Represents the filter.
    /// </summary>
    public static class Filter
    {
        /// <summary>
        /// Constructs with filter operation.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="filterOperation">Filter operation.</param>
        /// <returns>Filter.</returns>
        public static Filter<TEntity> With<TEntity>(QueryOperation<TEntity> filterOperation)
        {
            return Filter<TEntity>.With(filterOperation);
        }
    }
}
