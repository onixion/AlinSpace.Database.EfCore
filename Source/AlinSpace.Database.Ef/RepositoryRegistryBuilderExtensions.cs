using System;

namespace AlinSpace.Database.Ef
{
    public static class RepositoryRegistryBuilderExtensions
    {
        public static RepositoryRegistryBuilder RegisterTenantRepository(
            this RepositoryRegistryBuilder repositoryRegistryBuilder,
            AbstractTenantDbContext dbContext) 
        {
            var repositoryKey = new RepositoryKey(typeof(Database.Models.Tenant));
            repositoryRegistryBuilder.Register(new RepositoryRegistration(repositoryKey, new Lazy<object>(() => new Repository<Database.Models.Tenant>(dbContext, dbContext.Tenant))));

            return repositoryRegistryBuilder;
        }
    }
}
