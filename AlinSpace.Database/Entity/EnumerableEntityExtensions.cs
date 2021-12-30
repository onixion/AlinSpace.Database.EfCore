using System;
using System.Collections.Generic;

namespace AlinSpace.Database
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
        /// <param name="isSoftDeleted">Is soft deleted.</param>
        public static void SetSoftDeletion(this IEnumerable<IEntity> entities, bool isSoftDeleted)
        {
            foreach(var entity in entities)
            {
                entity.IsDeleted = isSoftDeleted;
            }
        }

        /// <summary>
        /// Set modification timestamp for entities.
        /// </summary>
        /// <param name="entities">Entities.</param>
        /// <param name="modificationTimestamp">Modification timestamp.</param>
        /// <remarks>
        /// If <paramref name="modificationTimestamp"/> is set to null, UTC now will be used.
        /// </remarks>
        public static void SetModificationTimestamp(this IEnumerable<IEntity> entities, DateTimeOffset? modificationTimestamp = null)
        {
            modificationTimestamp = modificationTimestamp ?? DateTimeOffset.UtcNow;

            foreach (var entity in entities)
            {
                entity.ModificationTimestamp = modificationTimestamp;
            }
        }
    }
}
