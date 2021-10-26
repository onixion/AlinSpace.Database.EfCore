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
            var repositoryKey = typeof(Models.Tenant);
            repositoryRegistryBuilder.Register(new RepositoryRegistration(repositoryKey, new Lazy<object>(() => new Repository<Database.Models.Tenant>(dbContext, dbContext.Tenant))));

            return repositoryRegistryBuilder;
        }
    }
}
