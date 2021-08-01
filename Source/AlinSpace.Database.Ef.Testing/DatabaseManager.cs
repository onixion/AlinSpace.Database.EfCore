using Microsoft.EntityFrameworkCore;
using System.IO;

namespace AlinSpace.Database.Ef.Testing
{
    static class DatabaseManager
    {
        public static void Create()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlite("Data source=Test.db")
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .EnableServiceProviderCaching()
                .Options;

            var databaseContext = new DatabaseContext(dbContextOptions);
            databaseContext.Database.EnsureCreated();
        }

        public static void Delete()
        {
            File.Delete("Test.db");
        }
    }
}
