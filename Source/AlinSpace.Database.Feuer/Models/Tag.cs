using System.ComponentModel.DataAnnotations;

namespace AlinSpace.Database.Feuer.Models
{
    /// <summary>
    /// Represents the tag model.
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// Name.
        /// </summary>
        [Key]
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
