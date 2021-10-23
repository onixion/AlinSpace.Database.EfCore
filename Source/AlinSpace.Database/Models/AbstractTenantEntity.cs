using Microsoft.EntityFrameworkCore;
using System;

namespace AlinSpace.Database.Models
{
    /// <summary>
    /// Represents the tenant entity.
    /// </summary>
    public class AbstractTenantEntity : AbstractEntity, ITenantEntity
    {
        /// <summary>
        /// Gets or sets the tenant ID.
        /// </summary>
        public long? TenantId { get; set; }

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

            modelBuilder.Entity<AbstractTenantEntity>()
                .HasOne(x => x.Tenant)
                .WithMany()
                .HasForeignKey(x => x.TenantId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
