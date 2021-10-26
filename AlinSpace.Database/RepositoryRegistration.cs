using System;

namespace AlinSpace.Database
{
    /// <summary>
    /// Repository registration.
    /// </summary>
    public class RepositoryRegistration
    {
        /// <summary>
        /// Gets the repository key.
        /// </summary>
        public Type Key { get; }

        /// <summary>
        /// Gets the repository provider.
        /// </summary>
        public Lazy<object> RepositoryProvider { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public RepositoryRegistration(Type key, Lazy<object> repositoryProvider)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            RepositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
        }
    }
}
