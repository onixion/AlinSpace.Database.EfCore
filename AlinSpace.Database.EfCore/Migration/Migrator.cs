using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AlinSpace.Database.EfCore
{
    /// <summary>
    /// Represents the database migrator.
    /// </summary>
    public class Migrator
    {
        /// <summary>
        /// Perform migration.
        /// </summary>
        /// <param name="dbContext">Database context.</param>
        /// <param name="seeders">Optional seeders.</param>
        public static void Perform(DbContext dbContext, Seeders seeders = null)
        {
            var pendingMigrations = dbContext.Database.GetPendingMigrations();

            if (pendingMigrations != null && pendingMigrations.Any())
            {
                dbContext.Database.Migrate();
            }

            var transaction = TransactionFactory.Create(dbContext);

            if (seeders != null)
            {
                foreach (var seeder in seeders.GetSeeders())
                {
                    seeder.Seed(transaction);
                    seeder.SeedAsync(transaction).Wait();

                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Perform migration asynchronously.
        /// </summary>
        /// <param name="dbContext">Database context.</param>
        /// <param name="seeders">Optional seeders.</param>
        public static async Task PerformAsync(DbContext dbContext, Seeders seeders = null)
        {
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

            if (pendingMigrations != null && pendingMigrations.Any())
            {
                dbContext.Database.Migrate();
            }

            var transaction = TransactionFactory.Create(dbContext);

            if (seeders != null)
            {
                foreach (var seeder in seeders.GetSeeders())
                {
                    seeder.Seed(transaction);
                    await seeder.SeedAsync(transaction);

                    await transaction.CommitAsync();
                }
            }
        }
    }
}
