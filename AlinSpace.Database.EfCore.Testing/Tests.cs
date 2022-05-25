using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

                Models.Book book = null;

                using (var transaction = TransactionFactory.Create<DatabaseContext>(dbContextOptions))
                {
                    var bookRepository = transaction.GetRepository<Models.Book>();
                    var pageRepository = transaction.GetRepository<Models.Page>();
                    var chapterRepository = transaction.GetRepository<Models.Chapter>();

                    book = new Models.Book()
                    {
                        Name = "MyBook",
                        Pages = new List<Models.Page>()
                        {
                            new Models.Page()
                            {
                                Chapters = new List<Models.Chapter>()
                                {
                                    new Models.Chapter(),
                                    new Models.Chapter(),
                                    new Models.Chapter(),
                                }
                            },
                            new Models.Page()
                            {
                                Chapters = new List<Models.Chapter>()
                                {
                                    new Models.Chapter(),
                                    new Models.Chapter(),
                                }
                            }
                        }
                    };

                    await bookRepository.CreateOrUpdateAsync(book);
                    await transaction.CommitAsync();
                }

                book.Name = "MyBook2";

                using (var transaction = TransactionFactory.Create<DatabaseContext>(dbContextOptions))
                {
                    var bookRepository = transaction.GetRepository<Models.Book>();

                    await bookRepository.CreateOrUpdateAsync(book);
                    await transaction.CommitAsync();
                }

                using (var transaction = TransactionFactory.Create<DatabaseContext>(dbContextOptions))
                {
                    var bookRepository = transaction.GetRepository<Models.Book>();
                    var retrievedBook = await bookRepository.FindFirstAsync();

                    Assert.Equal("MyBook2", retrievedBook.Name);
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
