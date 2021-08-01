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
        /// Get key type.
        /// </summary>
        public Type KeyType { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public RepositoryKey(Type entityType, Type keyType)
        {
            EntityType = entityType ?? throw new ArgumentNullException(nameof(entityType));
            KeyType = keyType ?? throw new ArgumentNullException(nameof(keyType));
        }

        /// <summary>
        /// Get hash code.
        /// </summary>
        public override int GetHashCode()
        {
            return EntityType.GetHashCode() ^ KeyType.GetHashCode();
        }
    }
}
