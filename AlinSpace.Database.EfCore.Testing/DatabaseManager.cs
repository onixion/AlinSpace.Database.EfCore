using Microsoft.EntityFrameworkCore;
using System.IO;

namespace AlinSpace.Database.EfCore.Testing
{
    static class DatabaseManager
    {
        public static readonly string ConnectionString = "Data source=Test.db";

        public static void Create()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlite(ConnectionString)
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
