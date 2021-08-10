using System.ComponentModel.DataAnnotations;

namespace AlinSpace.Database.Models
{
    /// <summary>
    /// Represents the abstract entity.
    /// </summary>
    /// <typeparam name="TId">Type of ID field.</typeparam>
    public abstract class AbstractEntity<TId>
    {
        /// <summary>
        /// Gets or sets the ID of the entity.
        /// </summary>
        [Key]
        public TId Id { get; set; }
    }
}
