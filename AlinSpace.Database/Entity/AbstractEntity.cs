using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AlinSpace.Database
{
    /// <summary>
    /// Represent the abstract default implementation of the entity.
    /// </summary>
    public abstract class AbstractEntity<TPrimaryKey, TTenantPrimaryKey> 
        : IEntityWithId<TPrimaryKey>, IEntityWithTenant<TTenantPrimaryKey>
        where TPrimaryKey : struct
        where TTenantPrimaryKey : struct
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
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        #endregion

        #region IEntityWithId

        /// <summary>
        /// Gets or sets the ID of the entity.
        /// </summary>
        [Key]
        public TPrimaryKey Id { get; set; }

        #endregion

        #region IEntityWithTenant

        /// <summary>
        /// Gets or sets the tenant ID.
        /// </summary>
        public TTenantPrimaryKey TenantId { get; set; }

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
            modelBuilder.Entity(entityType)
                .ToTable(entityName ?? entityType.Name);
        }
    }
}
