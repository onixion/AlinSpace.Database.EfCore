namespace AlinSpace.Database
{
    /// <summary>
    /// Represents the tenant entity interface.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of primary key.</typeparam>
    public interface ITenantEntity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Gets or sets the tenant ID.
        /// </summary>
        long? TenantId { get; set; }
    }
}
