using AlinSpace.Database.Feuer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AlinSpace.Database.Feuer.Console
{
    /// <summary>
    /// Represents the bootstrap static class.
    /// </summary>
    public static class Bootstrap
    {
        /// <summary>
        /// Sets up basic models.
        /// </summary>
        /// <param name="transaction">Transaction to execute the bootstrap on.</param>
        public static void Setup(ITransaction transaction)
        {
            var userRepository = transaction.GetRepository<User, long>();
            var pageRepository = transaction.GetRepository<Page, long>();
            var configurationRepository = transaction.GetRepository<Configuration, long>();

            var adminUser = CreateAdminUserIfNotExist(transaction, userRepository);
            CreateEssentialPages(transaction, configurationRepository, pageRepository, adminUser);
        }

        /// <summary>
        /// Creates an admin user if none exists. 
        /// </summary>
        /// <param name="transaction">Transaction.</param>
        /// <param name="userRepository">User repository.</param>
        /// <returns>An admin user.</returns>
        static User CreateAdminUserIfNotExist(ITransaction transaction, IRepository<User, long> userRepository)
        {
            // If there is at least one admin user, return.
            var optionalAdminUser = userRepository.Find(q => q.Where(u => u.Role == Role.Admin));
            if (optionalAdminUser.HasValue)
                return optionalAdminUser.Value;

            // Create a new admin user.
            var adminUser = new User()
            {
                Username = "alin",
                PasswordHash = "test", // TODO: read from config
                Role = Role.Admin,
                Firstname = "Alin",
                Lastname = "Andersen",
            };

            userRepository.Create(adminUser);
            transaction.Commit();

            return adminUser;
        }

        /// <summary>
        /// Creates essential pages.
        /// </summary>
        /// <param name="transaction">Transaction.</param>
        /// <param name="pageRepository">Page repository.</param>
        /// <param name="adminUser">Admin user.</param>
        private static void CreateEssentialPages(
            ITransaction transaction,
            IRepository<Configuration, long> configurationRepository,
            IRepository<Page, long> pageRepository, 
            User adminUser)
        {
            var indexPage = new Page()
            {
                Name = "Index page",
                Description = "This is the index page.",
                IsListed = false,
                Owner = adminUser,
            };

            pageRepository.Create(indexPage);

            var contactPage = new Page()
            {
                Name = "Index page",
                Description = "This is the index page.",
                IsListed = false,
                Owner = adminUser,
            };

            pageRepository.Create(indexPage);

            var aboutPage = new Page()
            {
                Name = "Index page",
                Description = "This is the index page.",
                IsListed = false,
                Owner = adminUser,
            };

            pageRepository.Create(indexPage);

            transaction.Commit();

            var configuration = configurationRepository
                .Get(
                    skip: 0,
                    take: 1,
                    func: q => q
                        .Include(c => c.IndexPage)
                        .Include(c => c.ContactPage)
                        .Include(c => c.AboutPage))
                .FirstOrDefault();

            // If configuration is not null, then check and overwrite
            // pages if not already set.
            if (configuration != null)
            {
                if (configuration.IndexPage == null)
                    configuration.IndexPage = indexPage;

                if (configuration.ContactPage == null)
                    configuration.ContactPage = contactPage;

                if (configuration.AboutPage == null)
                    configuration.AboutPage = aboutPage;
            }
            else
            {
                configuration = new Configuration()
                {
                    IndexPage = indexPage,
                    ContactPage = contactPage,
                    AboutPage = aboutPage,
                };

                configurationRepository.Create(configuration);
            }

            transaction.Commit();
        }
    }
}
