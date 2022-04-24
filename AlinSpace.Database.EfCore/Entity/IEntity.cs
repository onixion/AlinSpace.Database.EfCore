using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace AlinSpace.Database.EfCore
{
    /// <summary>
    /// Represents the entity interface.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the ID of the entity.
        /// </summary>
        [Key]
        long Id { get; set; }

        /// <summary>
        /// Gets or sets the tenant ID.
        /// </summary>
        long? TenantId { get; set; }

        /// <summary>
        /// Gets or sets the delete flag.
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets meta data.
        /// </summary>
        string MetaData { get; set; }

        #region Timestamps

        /// <summary>
        /// Gets or sets the creation timestamp.
        /// </summary>
        /// <remarks>
        /// In UTC.
        /// </remarks>
        DateTimeOffset? CreationTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the modification timestamp.
        /// </summary>
        /// <remarks>
        /// In UTC.
        /// </remarks>
        DateTimeOffset? ModificationTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the deletion timestamp.
        /// </summary>
        /// <remarks>
        /// In UTC.
        /// </remarks>
        DateTimeOffset? DeletionTimestamp { get; set; }

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
        void OnModelCreating(ModelBuilder modelBuilder, Type entityType, string entityName = null);
    }
}
