namespace AlinSpace.Database.Feuer.Models
{
    /// <summary>
    /// Represents the page model.
    /// </summary>
    public class Page
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the page name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        public User Owner { get; set; }
    }
}
