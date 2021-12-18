using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace AlinSpace.Database
{
    /// <summary>
    /// Represents the entity interface.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of primary key.</typeparam>
    public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Gets or sets the ID of the entity.
        /// </summary>
        [Key]
        TPrimaryKey Id { get; set; }

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
        /// Gets or sets meta data.
        /// </summary>
        string MetaData { get; set; }

        /// <summary>
        /// Gets or sets the delete flag.
        /// </summary>
        bool IsDeleted { get; set; }

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
