using System;
using System.Linq;
using System.Linq.Expressions;

namespace AlinSpace.Database
{
    /// <summary>
    /// Extensions for <see cref="ScopeTenant(IQueryable{AbstractTenantEntity}, long)<"/>
    /// </summary>
    public static class QuerableExtensions
    {
        /// <summary>
        /// Scope querable for tenant.
        /// </summary>
        /// <typeparam name="TTenantEntity">Type of tenant entity.</typeparam>
        /// <typeparam name="TPrimaryKey">Type of primary key.</typeparam>
        /// <param name="queryable">Queryable.</param>
        /// <param name="tenantId">Tenant ID.</param>
        /// <returns>Queryable scope to given tenant.</returns>
        public static IQueryable<TTenantEntity> ScopeTenant<TTenantEntity, TPrimaryKey>(
            this IQueryable<TTenantEntity> queryable,
            long tenantId) where TTenantEntity : ITenantEntity<TPrimaryKey>
        {
            return queryable.Where(x => x.TenantId == tenantId);
        }

        /// <summary>
        /// Where entities are not deleted.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <typeparam name="TPrimaryKey">Type of primary key.</typeparam>
        /// <param name="queryable">Queryable.</param>
        /// <returns>Queryable with not deleted entities.</returns>
        public static IQueryable<TEntity> WhereNotSoftDeleted<TEntity, TPrimaryKey>(
            this IQueryable<TEntity> queryable) where TEntity : IEntity<TPrimaryKey>
        {
            return queryable.Where(x => x.IsDeleted == false);
        }

        /// <summary>
        /// Get a specific page.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="queryable">Queryable.</param>
        /// <param name="page">Page number.</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="predicate">Optional predicate.</param>
        /// <returns>Queryable of the page.</returns>
        public static IQueryable<TEntity> GetPage<TEntity>(
            this IQueryable<TEntity> queryable,
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>> predicate = null)
            where TEntity : class
        {
            if (page <= 0)
                page = 1;

            if (pageSize <= 0)
                pageSize = 1;

            if (predicate != null)
                queryable = queryable.Where(predicate);

            return queryable
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
