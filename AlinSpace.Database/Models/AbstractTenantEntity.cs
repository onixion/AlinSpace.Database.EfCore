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
        public long TenantId { get; set; }

        /// <summary>
        /// Gets or sets the tenant.
        /// </summary>
        public Tenant Tenant { get; set; }

        /// <summary>
        /// On model creating.
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
        /// <param name="entityType">Entity type.</param>
        public override void OnModelCreating(ModelBuilder modelBuilder, Type entityType)
        {
            base.OnModelCreating(modelBuilder, entityType);

            modelBuilder.Entity<AbstractTenantEntity<TPrimaryKey>>()
                .HasOne(x => x.Tenant)
                .WithMany()
                .HasForeignKey(x => x.TenantId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
