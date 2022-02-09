using System;

namespace AlinSpace.Database
{
    public class Filter<TEntity>
    {
        public QueryOperation<TEntity> FilterOperation { get; private set; }

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
}
