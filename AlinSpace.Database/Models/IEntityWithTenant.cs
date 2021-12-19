namespace AlinSpace.Database
{
    /// <summary>
    /// Represents the entity with tenant interface.
    /// </summary>
    /// <typeparam name="TTenantPrimaryKey">Type of tenant primary key.</typeparam>
    public interface IEntityWithTenant<TTenantPrimaryKey> : IEntity where TTenantPrimaryKey : struct
    {
        /// <summary>
        /// Gets or sets the tenant ID.
        /// </summary>
        TTenantPrimaryKey TenantId { get; set; }
    }
}
