namespace AlinSpace.Database
{
    /// <summary>
    /// Represents the query options.
    /// </summary>
    public class QueryOptions
    {
        /// <summary>
        /// Gets or sets whether or not tracking is enabled.
        /// </summary>
        public bool Tracking { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public QueryOptions(bool tracking = false)
        {
            Tracking = tracking;
        }

        /// <summary>
        /// Gets the query options with tracking enabled.
        /// </summary>
        public static QueryOptions WithTracking { get; } = new QueryOptions(true);
    }
}
