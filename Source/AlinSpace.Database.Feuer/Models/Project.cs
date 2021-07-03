using System.ComponentModel.DataAnnotations;

namespace AlinSpace.Database.Feuer.Models
{
    /// <summary>
    /// Represents the project model.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        [Key]
        [Required]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [MaxLength(100)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the flag indicating whether or not the group is public accessible.
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// Gets or sets the flag indicating whether or not the group is listed in the search.
        /// </summary>
        public bool IsListed { get; set; }

        /// <summary>
        /// Priority.
        /// </summary>
        /// <remarks>
        /// The higher the priority, the higher the value.
        /// </remarks>
        public long Priority { get; set; }

        /// <summary>
        /// URL.
        /// </summary>
        [MaxLength(100)]
        public string Url { get; set; }

        /// <summary>
        /// Repository URL.
        /// </summary>
        [MaxLength(100)]
        public string RepositoryUrl { get; set; }

        /// <summary>
        /// Page.
        /// </summary>
        public Page Page { get; set; }

        /// <summary>
        /// Owner.
        /// </summary>
        [Required]
        public User Owner { get; set; }
    }
}
