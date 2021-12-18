using Microsoft.EntityFrameworkCore;
using System;

namespace AlinSpace.Database
{
    /// <summary>
    /// Represents the abstract tenant entity.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of primary key.</typeparam>
    public abstract class AbstractTenantEntity<TPrimaryKey> : AbstractEntity<TPrimaryKey>, ITenantEntity<TPrimaryKey>
    {
        /// <summary>
        /// Gets or sets the tenant ID.
        /// </summary>
        public long? TenantId { get; set; }

        /// <summary>
        /// On model creating.
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
        /// <param name="entityType">Type of entity.</param>
        /// <param name="entityName">Optional name of entity.</param>
        /// <remarks>
        /// If <paramref name="entityName"/> is not set, the name of the type will be taken.
        /// </remarks>
        public override void OnModelCreating(ModelBuilder modelBuilder, Type entityType, string entityName = null)
        {
            base.OnModelCreating(modelBuilder, entityType);

            // Table-per-type inheritance handling.
            modelBuilder.Entity(entityType)
                .ToTable(entityName ?? entityType.Name);
        }
    }
}
