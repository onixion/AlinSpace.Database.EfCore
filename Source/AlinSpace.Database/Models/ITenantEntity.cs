using System.ComponentModel.DataAnnotations;

namespace AlinSpace.Database.Models
{
    /// <summary>
    /// Represents the tenant entity interface.
    /// </summary>
    public interface ITenantEntity : IEntity
    {
        /// <summary>
        /// Gets or sets the tenant ID.
        /// </summary>
        long? TenantId { get; set; }

        /// <summary>
        /// Gets or sets the tenant.
        /// </summary>
        Tenant Tenant { get; set; }
    }
}
