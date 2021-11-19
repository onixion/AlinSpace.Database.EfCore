namespace AlinSpace.Database
{
    /// <summary>
    /// Represents the tenant model.
    /// </summary>
    public class Tenant : AbstractEntity<long>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}
