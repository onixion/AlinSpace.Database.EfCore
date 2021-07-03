using System.ComponentModel.DataAnnotations;

namespace AlinSpace.Database.Feuer.Models
{
    /// <summary>
    /// Represents the configuration model.
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        [Key]
        [Required]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the index page.
        /// </summary>
        public Page IndexPage { get; set; }

        /// <summary>
        /// Gets or sets the contact page.
        /// </summary>
        public Page ContactPage { get; set; }

        /// <summary>
        /// Gets or sets the about page.
        /// </summary>
        public Page AboutPage { get; set; }
    }
}
