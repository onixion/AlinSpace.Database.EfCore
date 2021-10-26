using AlinSpace.Database.Models;
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
        /// <param name="queryable">Queryable.</param>
        /// <param name="tenantId">Tenant ID.</param>
        /// <returns>Queryable scopped to given tenant.</returns>
        public static IQueryable<ITenantEntity> ScopeTenant(
            this IQueryable<AbstractTenantEntity> queryable,
            long tenantId)
        {
            return queryable.Where(x => x.TenantId == tenantId);
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
            {
                queryable = queryable.Where(predicate);
            }

            return queryable
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
