using System.Collections.Generic;

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
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password hash.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets pages.
        /// </summary>
        public IList<Page> Pages { get; set; }
    }
}
