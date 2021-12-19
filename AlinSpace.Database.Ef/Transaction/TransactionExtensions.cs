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
        /// <typeparam name="TEntityWithId">Type of the entity with ID.</typeparam>
        /// <typeparam name="TPrimaryKey">Type of primary key.</typeparam>
        /// <param name="transaction">Transaction.</param>
        /// <returns>CRUD repository.</returns>
        public static ICrudRepository<TEntityWithId, TPrimaryKey> GetCrudRepository<TEntityWithId, TPrimaryKey>(
            this ITransaction transaction)
            where TEntityWithId : class, IEntityWithId<TPrimaryKey>
            where TPrimaryKey : struct
        {
            return new CrudRepository<TEntityWithId, TPrimaryKey>(transaction);
        }
    }
}
