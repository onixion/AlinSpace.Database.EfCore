using System;
using System.Collections.Generic;

namespace AlinSpace.Database.EfCore
{
    /// <summary>
    /// Extensions for <see cref="IEnumerable{IEntity}"/>.
    /// </summary>
    public static class EnumerableEntityExtensions
    {
        /// <summary>
        /// Set soft deletion for entities.
        /// </summary>
        /// <param name="entities">Entities.</param>
        /// <param name="isSoftDeleted">Soft deleted flag.</param>
        public static void SetSoftDeleted(this IEnumerable<IEntity> entities, bool isSoftDeleted)
        {
            foreach(var entity in entities)
            {
                entity.IsDeleted = isSoftDeleted;
            }
        }

        /// <summary>
        /// Updates the modification timestamp for entities.
        /// </summary>
        /// <param name="entities">Entities.</param>
        /// <param name="modificationTimestamp">Optionalm modification timestamp.</param>
        /// <remarks>
        /// If <paramref name="modificationTimestamp"/> is set to null, UtcNow will be used.
        /// </remarks>
        public static void UpdateModificationTimestamp(this IEnumerable<IEntity> entities, DateTimeOffset? modificationTimestamp = null)
        {
            modificationTimestamp ??= DateTimeOffset.UtcNow;

            foreach (var entity in entities)
            {
                entity.ModificationTimestamp = modificationTimestamp;
            }
        }
    }
}
