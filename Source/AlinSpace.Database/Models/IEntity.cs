using System.ComponentModel.DataAnnotations;

namespace AlinSpace.Database.Models
{
    /// <summary>
    /// Represents the entity interface.
    /// </summary>
    /// <typeparam name="TId">Type of the ID field.</typeparam>
    public interface IEntity<TId>
    {
        /// <summary>
        /// Gets or sets the ID of the entity.
        /// </summary>
        [Key]
        TId Id { get; set; }
    }
}
