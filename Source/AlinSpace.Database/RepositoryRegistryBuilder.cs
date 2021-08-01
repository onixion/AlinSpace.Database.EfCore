using System.Collections.Generic;

namespace AlinSpace.Database
{
    /// <summary>
    /// Represents the repository registry builder.
    /// </summary>
    public class RepositoryRegistryBuilder
    {
        /// <summary>
        /// Gets the registrations.
        /// </summary>
        public IList<RepositoryRegistration> Registrations { get; } = new List<RepositoryRegistration>();

        /// <summary>
        /// Register the given repository registration.
        /// </summary>
        /// <param name="repositoryRegistration">Repository registration to register.</param>
        public RepositoryRegistryBuilder Register(RepositoryRegistration repositoryRegistration)
        {
            Registrations.Add(repositoryRegistration);
            return this;
        }

        /// <summary>
        /// Builds the repository registry.
        /// </summary>
        /// <returns>Repository registry.</returns>
        public RepositoryRegistry Build()
        {
            return new RepositoryRegistry(Registrations);
        }
    }
}
