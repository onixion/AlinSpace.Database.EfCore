using AlinSpace.Database.Models;
using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AlinSpace.Database.Ef.Testing
{
    public class Tests
    {
        [Fact]
        public void Test()
        {
            DatabaseManager.Delete();
            DatabaseManager.Create();

            try
            {
                var factory = TransactionFactoryBuilder<DatabaseContext>
                    .New()
                    .WithRegistry((c, b) =>
                    {
                        b.RegisterTenantRepository(c);
                        b.Register(() => new Repository<Models.Book>(c, c.Book));
                        b.Register(() => new Repository<Models.Page>(c, c.Page));
                    })
                    .WithTransaction((c, r) =>  new Transaction(c, r))
                    .Build();
               
                var dbContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                    .UseSqlite(DatabaseManager.ConnectionString)
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                    .EnableServiceProviderCaching()
                    .Options;

                var databaseContext = new DatabaseContext(dbContextOptions);

                using (var transaction = factory.CreateTransaction(databaseContext))
                {
                    var bookRepository = transaction.GetRepository<Models.Book>();
                    var pageRepository = transaction.GetRepository<Models.Page>();
                    



                    Console.ReadKey();
                }
            }
            catch(Exception e)
            {
                throw;
            }
            finally
            {
                DatabaseManager.Delete();
            }
        }
    }
}
