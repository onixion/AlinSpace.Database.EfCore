namespace AlinSpace.Database.Ef
{
    /// <summary>
    /// Extensions for <see cref="ITransaction"/>.
    /// </summary>
    public static class TransactionExtensions
    {
        /// <summary>
        /// Gets CRUD repository.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <typeparam name="TPrimaryKey">Type of primary key.</typeparam>
        /// <param name="transaction">Transaction.</param>
        /// <returns>CRUD repository.</returns>
        public static ICrudRepository<TEntity, TPrimaryKey> GetCrudRepository<TEntity, TPrimaryKey>(
            this ITransaction transaction)
            where TEntity : class, IEntity<TPrimaryKey>
            where TPrimaryKey : struct
        {
            return new CrudRepository<TEntity, TPrimaryKey>(transaction);
        }
    }
}
