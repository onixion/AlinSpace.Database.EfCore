using System;

namespace AlinSpace.Database
{
    public class Includer<TEntity>
    {
        public QueryOperation<TEntity> IncluderOperation { get; private set; }

        public static Includer<TEntity> With(QueryOperation<TEntity> includerOperation)
        {
            if (includerOperation == null)
                throw new ArgumentNullException(nameof(includerOperation));

            return new Includer<TEntity>()
            {
                IncluderOperation = includerOperation,
            };
        }
    }
}
