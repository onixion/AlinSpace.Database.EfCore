using AlinSpace.Database.Feuer.Models;
using Microsoft.EntityFrameworkCore;

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


            var userRepository = transaction.GetRepository<User, long>();

            var user = new User()
            {
                Username = "TEST",
                FirstName = "Alin",
                LastName = "Andersen",
            };

            userRepository.Create(user);

            var t = transaction.Commit();


        }
    }
}
