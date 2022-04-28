using System;
using System.Linq;

namespace AlinSpace.Database.EfCore
{
    /// <summary>
    /// Extensions for <see cref="ScopeTenant(IQueryable{AbstractTenantEntity}, long)<"/>
    /// </summary>
    public static class QuerableExtensions
    {
        #region Tenant

        /// <summary>
        /// Filter querable for tenant ID equals to the value given.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="queryable">Queryable.</param>
        /// <param name="tenantId">Tenant ID.</param>
        /// <returns>Queryable.</returns>
        public static IQueryable<TEntity> WhereTenant<TEntity>(
            this IQueryable<TEntity> queryable,
            long? tenantId) 
            where TEntity : IEntity
        {
            return queryable.Where(x => x.TenantId.Equals(tenantId));
        }

        /// <summary>
        /// Filters querable for tenant ID equals to null or the value given.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="queryable">Queryable.</param>
        /// <param name="tenantId">Tenant ID.</param>
        /// <returns>Queryable.</returns>
        public static IQueryable<TEntity> WhereTenantOrNull<TEntity>(
            this IQueryable<TEntity> queryable,
            long? tenantId)
            where TEntity : IEntity
        {
            return queryable.Where(x => !x.TenantId.HasValue || x.TenantId.Equals(tenantId) );
        }

        #endregion

        #region Deletion

        /// <summary>
        /// Filter entities that are soft deleted.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="queryable">Queryable.</param>
        /// <returns>Queryable.</returns>
        public static IQueryable<TEntity> WhereSoftDeleted<TEntity>(
            this IQueryable<TEntity> queryable) where TEntity : IEntity
        {
            return queryable.Where(x => x.IsDeleted == true);
        }

        /// <summary>
        /// Filter entities that are not soft deleted.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="queryable">Queryable.</param>
        /// <returns>Queryable with not soft deleted entities.</returns>
        public static IQueryable<TEntity> WhereNotSoftDeleted<TEntity>(
            this IQueryable<TEntity> queryable) where TEntity : IEntity
        {
            return queryable.Where(x => x.IsDeleted == false);
        }

        #endregion

        /// <summary>
        /// Takes a page from the queryable.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="queryable">Queryable.</param>
        /// <param name="pager">Pager to use to retrieve the page.</param>
        /// <returns>Page of the queryable.</returns>
        public static IQueryable<TEntity> TakePage<TEntity>(
            this IQueryable<TEntity> queryable,
            Pager pager) where TEntity : IEntity
        {
            if (pager == null)
                throw new ArgumentNullException(nameof(pager));

            return pager.TakePage(queryable);
        }

        /// <summary>
        /// Where timestamp is in the given range.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="queryable">Queryable.</param>
        /// <param name="timestampSelector">Timestamp selector.</param>
        /// <param name="from">From timestamp.</param>
        /// <param name="to">To timestamp.</param>
        /// <returns>Queryable.</returns>
        public static IQueryable<TEntity> WhereTimestamp<TEntity>(
            this IQueryable<TEntity> queryable,
            Func<TEntity, DateTimeOffset> timestampSelector,
            DateTimeOffset from,
            DateTimeOffset to) where TEntity : IEntity
        {
            return queryable.Where(x =>
                timestampSelector(x) >= from &&
                timestampSelector(x) <= to);
        }
    }
}
