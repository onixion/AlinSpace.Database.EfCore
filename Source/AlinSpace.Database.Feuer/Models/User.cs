using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AlinSpace.Database.Feuer.Models
{
    /// <summary>
    /// Represents the user model.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        [Key]
        [Required]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string Username { get; set; }

        /// <summary>
        /// Password hash.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the role of the user.
        /// </summary>
        [DefaultValue(Role.Guest)]
        public Role Role { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [MaxLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [MaxLength(20)]
        public string Firstname { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [MaxLength(20)]
        public string Lastname { get; set; }

        /// <summary>
        /// Gets or sets pages.
        /// </summary>
        public IList<Page> Pages { get; set; } = new List<Page>();

        /// <summary>
        /// Gets or sets page groups.
        /// </summary>
        public IList<PageGroup> PageGroups { get; set; } = new List<PageGroup>();

        /// <summary>
        /// Gets or sets projects.
        /// </summary>
        public IList<Project> Projects { get; set; } = new List<Project>();
    }
}
