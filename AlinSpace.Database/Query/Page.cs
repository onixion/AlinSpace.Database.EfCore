using System.Linq;

namespace AlinSpace.Database
{
    /// <summary>
    /// Represents the pager.
    /// </summary>
    public class Pager
    {
        /// <summary>
        /// Page number.
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// Count of items on the page.
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Number of items to skip to be on the page.
        /// </summary>
        public int Skip => (Number - 1) * Count;

        /// <summary>
        /// Number of items to take.
        /// </summary>
        public int Take => Count;

        private Pager(int number, int count)
        {
            if (number < 1)
                number = 1;

            if (count < 1)
                count = 1;

            Number = number;
            Count = count;
        }

        /// <summary>
        /// Construct pager with page number and page size.
        /// </summary>
        /// <param name="number">Number of the page.</param>
        /// <param name="count">Count of items on the page.</param>
        /// <returns>Pager.</returns>
        public static Pager With(int number, int count)
        {
            return new Pager(number, count);
        }

        /// <summary>
        /// Take the page.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="query">Query.</param>
        /// <returns>Query.</returns>
        public IQueryable<TEntity> TakePage<TEntity>(IQueryable<TEntity> query)
        {
            return query.Skip(Skip).Take(Take);
        }
    }
}
