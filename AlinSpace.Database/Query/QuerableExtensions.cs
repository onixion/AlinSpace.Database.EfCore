﻿using System.Linq;

namespace AlinSpace.Database
{
    /// <summary>
    /// Extensions for <see cref="ScopeTenant(IQueryable{AbstractTenantEntity}, long)<"/>
    /// </summary>
    public static class QuerableExtensions
    {
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
    }
}
