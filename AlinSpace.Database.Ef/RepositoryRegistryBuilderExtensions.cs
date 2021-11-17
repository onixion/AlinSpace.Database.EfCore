using System;

namespace AlinSpace.Database.Ef
{
    /// <summary>
    /// Extensions for <see cref="RepositoryRegistryBuilder"/>.
    /// </summary>
    public static class RepositoryRegistryBuilderExtensions
    {
        /// <summary>
        /// Register tenant repository.
        /// </summary>
        /// <param name="repositoryRegistryBuilder">Repository registry builder.</param>
        /// <param name="dbContext">Tenant database context.</param>
        public static RepositoryRegistryBuilder RegisterTenantRepository(
            this RepositoryRegistryBuilder repositoryRegistryBuilder,
            AbstractTenantDbContext dbContext) 
        {
            repositoryRegistryBuilder.Register(new RepositoryRegistration(typeof(Tenant), new Lazy<object>(() => new Repository<Tenant>(dbContext, dbContext.Tenant))));

            return repositoryRegistryBuilder;
        }
    }
}
