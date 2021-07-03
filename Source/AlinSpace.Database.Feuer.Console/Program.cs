using AlinSpace.Database.Feuer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AlinSpace.Database.Feuer.Console
{
    /// <summary>
    /// Program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main.
        /// </summary>
        /// <param name="args">Arguments.</param>
        static void Main(string[] args)
        {
            var dbContextBuilder = new DbContextOptionsBuilder<DatabaseContext>()
                .UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=postgres");

            var databaseContext = new DatabaseContext(dbContextBuilder.Options);

            using var transaction = new Transaction(databaseContext);

            Bootstrap.Setup(transaction);
            RetrieveAndPrintBootstrapData(transaction);
        }

        static void RetrieveAndPrintBootstrapData(ITransaction transaction)
        {
            var userRepository = transaction.GetRepository<User, long>();
            var adminUser = userRepository.Find(
                q => q
                    .Where(u => u.Role == Role.Admin)
                    .Include(u => u.Pages));

            if (adminUser == null)
            {
                System.Console.WriteLine($"No admin user found.");
            }
            else
            {
                System.Console.WriteLine($"Admin:");
                System.Console.WriteLine($"  Id        = {adminUser.Id}");
                System.Console.WriteLine($"  Username  = {adminUser.Username}");
                System.Console.WriteLine($"  Firstname = {adminUser.Firstname}");
                System.Console.WriteLine($"  Lastname  = {adminUser.Lastname}");
                System.Console.WriteLine($"  Pages     = {adminUser.Pages.Count}");
            }

            var configurationRepository = transaction.GetRepository<Configuration, long>();

            var configuration = configurationRepository.Find(q
                => q.Include(c => c.IndexPage)
                    .Include(c => c.ContactPage)
                    .Include(c => c.AboutPage));

            if (configuration == null)
            {
                System.Console.WriteLine($"No configuration found.");
            }
            else
            {
                System.Console.WriteLine($"Configuration:");
                System.Console.WriteLine($"  IndexPage   = {configuration.IndexPage.Id}");
                System.Console.WriteLine($"  ContactPage = {configuration.ContactPage.Id}");
                System.Console.WriteLine($"  AboutPage   = {configuration.AboutPage.Id}");
            }

            var pageRepository = transaction.GetRepository<Page, long>();
        }
    }
}
