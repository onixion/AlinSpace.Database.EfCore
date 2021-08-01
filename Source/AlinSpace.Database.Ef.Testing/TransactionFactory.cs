using AlinSpace.Database.Ef.Testing.Models;
using Microsoft.EntityFrameworkCore;

namespace AlinSpace.Database.Ef.Testing
{
    public static class TransactionFactory
    {
        public static ITransaction CreateTransaction(bool ensureCreated = false)
        {
            var dbContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlite("Data source=Test.db")
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .EnableServiceProviderCaching()
                .Options;

            var databaseContext = new DatabaseContext(dbContextOptions);

            var repositoryRegistry = new RepositoryRegistryBuilder()
                .Register(() => new Repository<Order, long>(databaseContext, databaseContext.Orders))
                .Register(() => new Repository<Person, long>(databaseContext, databaseContext.Persons))
                .Register(() => new Repository<Product, long>(databaseContext, databaseContext.Products))
                .Build();

            return new Transaction(databaseContext, repositoryRegistry);
        }
    }
}
