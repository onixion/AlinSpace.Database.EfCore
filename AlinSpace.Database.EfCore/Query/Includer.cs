using System;

namespace AlinSpace.Database.EfCore
{
    /// <summary>
    /// Represents the includer.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    public class Includer<TEntity>
    {
        /// <summary>
        /// Gets or sets the includer operation.
        /// </summary>
        public QueryOperation<TEntity> IncluderOperation { get; private set; }

        /// <summary>
        /// Constructs with includer operation.
        /// </summary>
        /// <param name="includerOperation">Includer operation.</param>
        /// <returns>Includer.</returns>
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

    /// <summary>
    /// Represents the includer.
    /// </summary>
    public static class Includer
    {
        /// <summary>
        /// Constructs with includer operation.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="includerOperation">Includer operation.</param>
        /// <returns>Includer.</returns>
        public static Includer<TEntity> With<TEntity>(QueryOperation<TEntity> includerOperation)
        {
            return Includer<TEntity>.With(includerOperation);
        }
    }
}
