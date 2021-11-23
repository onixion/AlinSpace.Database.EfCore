using System;

namespace AlinSpace.Database
{
    /// <summary>
    /// Represents the entity not found exception.
    /// </summary>
    public class EntityNotFoundException : Exception
    {
        /// <summary>
        /// Gets the primary key of the entity that could not be found.
        /// </summary>
        public object PrimaryKey { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public EntityNotFoundException(object primaryKey)
        {
            PrimaryKey = primaryKey;
        }
    }
}
