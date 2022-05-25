using Microsoft.EntityFrameworkCore;
using System;

namespace AlinSpace.Database.EfCore
{
    /// <summary>
    /// Represent the abstract default implementation of the entity.
    /// </summary>
    public abstract class AbstractEntity : IEntity
    {
        /// <summary>
        /// Gets or sets the ID of the entity.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the tenant ID.
        /// </summary>
        public long? TenantId { get; set; }

        /// <summary>
        /// Gets or sets the delete flag.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets meta data.
        /// </summary>
        public string MetaData { get; set; }

        #region Timestamps

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
        /// Gets or sets the deletion timestamp.
        /// </summary>
        /// <remarks>
        /// In UTC.
        /// </remarks>
        public DateTimeOffset? DeletionTimestamp { get; set; }

        #endregion

        /// <summary>
        /// On model creating.
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
        /// <param name="entityType">Type of entity.</param>
        public void OnModelCreating(ModelBuilder modelBuilder, Type entityType)
        {
            // Table-per-type inheritance handling.
            modelBuilder
                .Entity(entityType)
                .ToTable(entityType.FullName);

            modelBuilder
                .Entity(entityType)
                .HasIndex(nameof(Id));

            modelBuilder
                .Entity(entityType)
                .Property(nameof(TenantId))
                .HasDefaultValue(null)
                .IsRequired(false);

            modelBuilder
                .Entity(entityType)
                .Property(nameof(IsDeleted))
                .HasDefaultValue(false)
                .IsRequired(true);
        }

        protected virtual void CreateModel(ModelBuilder modelBuilder, Type entityType)
        {
        }
    }
}
