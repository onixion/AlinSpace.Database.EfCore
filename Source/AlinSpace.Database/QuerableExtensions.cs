using AlinSpace.Database.Models;
using System.Linq;

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
    }
}
