using System;

namespace AlinSpace.Database
{
    /// <summary>
    /// Repository key.
    /// </summary>
    public class RepositoryKey
    {
        /// <summary>
        /// Get entity type.
        /// </summary>
        public Type EntityType { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public RepositoryKey(Type entityType)
        {
            EntityType = entityType ?? throw new ArgumentNullException(nameof(entityType));
        }

        /// <summary>
        /// Get hash code.
        /// </summary>
        public override int GetHashCode()
        {
            return EntityType.GetHashCode();
        }
    }
}
