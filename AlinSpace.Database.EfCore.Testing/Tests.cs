using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace AlinSpace.Database.EfCore.Testing
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
                var dbContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                    .UseSqlite(DatabaseManager.ConnectionString)
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                    .EnableServiceProviderCaching()
                    .Options;

                using (var transaction = TransactionFactory.Create<DatabaseContext>(dbContextOptions))
                {
                    var bookRepository = transaction.GetRepository<Models.Book>();
                    var pageRepository = transaction.GetRepository<Models.Page>();

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
