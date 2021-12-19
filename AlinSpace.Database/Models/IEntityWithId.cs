using System.ComponentModel.DataAnnotations;

namespace AlinSpace.Database
{
    /// <summary>
    /// Represents the entity with ID interface.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of primary key.</typeparam>
    public interface IEntityWithId<TPrimaryKey> : IEntity where TPrimaryKey : struct
    {
        /// <summary>
        /// Gets or sets the ID of the entity.
        /// </summary>
        [Key]
        TPrimaryKey Id { get; set; }
    }
}
