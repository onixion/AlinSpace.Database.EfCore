using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AlinSpace.Database.EfCore.Testing
{
    public class Tests
    {
        [Fact]
        public async Task TestAsync()
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


                    var book1 = new Models.Book();
                    var book2 = new Models.Book();
                    book1.Id = 10;
                    bookRepository.Update(book1);
                    await transaction.CommitAsync();


                    var count3 = bookRepository.FindMany().Count();


                }


                using (var transaction = TransactionFactory.Create<DatabaseContext>(dbContextOptions))
                {
                    var bookRepository = transaction.GetRepository<Models.Book>();

                    var count1 = bookRepository.FindMany().Count();

                    var book = bookRepository.Get(1);
                    bookRepository.Delete(book);

                    await transaction.CommitAsync();

                    var count3 = bookRepository.FindMany().Count();
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
