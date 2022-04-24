using System.Linq;

namespace AlinSpace.Database.EfCore
{
    /// <summary>
    /// Represents the query operation.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity.</typeparam>
    /// <param name="queryable">Queryable.</param>
    /// <returns>Queryable with operation.</returns>
    public delegate IQueryable<TEntity> QueryOperation<TEntity>(IQueryable<TEntity> queryable);
}
