using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AlinSpace.Database.Feuer.Console
{
    /// <summary>
    /// Represents the database context factory.
    /// </summary>
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        /// <summary>
        /// Create database context.
        /// </summary>
        /// <param name="args">Arguments.</param>
        /// <returns>Database context.</returns>
        public DatabaseContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder()
                .UseNpgsql(
                    connectionString: "Host=127.0.0.1;Port=5432;Username=postgres;Password=postgres",
                    npgsqlOptionsAction: b => b.MigrationsAssembly("AlinSpace.Database.Feuer.Console"))
                .Options;

            return new DatabaseContext(options);
        }
    }
}
