using Microsoft.EntityFrameworkCore;
using System;

namespace AlinSpace.Database
{
    /// <summary>
    /// Represents the abstract entity with tenant implementation.
    /// </summary>
    /// <typeparam name="TTenantPrimaryKey">Type of the tenant primary key.</typeparam>
    public abstract class AbstractEntityWithTenant<TTenantPrimaryKey> : IEntityWithTenant<TTenantPrimaryKey> where TTenantPrimaryKey : struct
    {
        #region IEntity

        /// <summary>
        /// Gets or sets the creation timestamp.
        /// </summary>
        /// <remarks>
        /// In UTC.
        /// </remarks>
        public DateTimeOffset? CreationTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the modification timestamp.
        /// </summary>
        /// <remarks>
        /// In UTC.
        /// </remarks>
        public DateTimeOffset? ModificationTimestamp { get; set; }

        /// <summary>
        /// Gets or sets meta data.
        /// </summary>
        public string MetaData { get; set; }

        /// <summary>
        /// Gets or sets the delete flag.
        /// </summary>
        public bool IsDeleted { get; set; }

        #endregion

        #region IEntityWithTenant

        /// <summary>
        /// Gets or sets the tenant ID.
        /// </summary>
        public TTenantPrimaryKey? TenantId { get; set; }

        #endregion

        /// <summary>
        /// On model creating.
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
        /// <param name="entityType">Type of entity.</param>
        /// <param name="entityName">Optional name of entity.</param>
        /// <remarks>
        /// If <paramref name="entityName"/> is not set, the name of the type will be taken.
        /// </remarks>
        public virtual void OnModelCreating(ModelBuilder modelBuilder, Type entityType, string entityName = null)
        {
            // Table-per-type inheritance handling.
            modelBuilder
                .Entity(entityType)
                .ToTable(entityName ?? entityType.Name);

            #region IEntity

            modelBuilder
                .Entity(entityName)
                .Property(nameof(IsDeleted))
                .HasDefaultValue(false)
                .IsRequired(true);

            #endregion

            #region IEntityWithTenant

            modelBuilder
                .Entity(entityName)
                .Property(nameof(TenantId))
                .HasDefaultValue(null)
                .IsRequired(false);

            #endregion
        }
    }
}
