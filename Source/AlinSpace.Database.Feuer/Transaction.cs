using AlinSpace.Database.Ef;
using AlinSpace.Database.Feuer.Models;

namespace AlinSpace.Database.Feuer
{
    /// <summary>
    /// Represents the transaction.
    /// </summary>
    public class Transaction : Ef.AbstractTransaction
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="databaseContext">Database context.</param>
        public Transaction(DatabaseContext databaseContext) : base(databaseContext)
        {
            // Configuration
            RegisterRepository(()
                => new Repository<Configuration, long>(
                    dbSet: databaseContext.Configuration,
                    modelKeyFunc: (m, k) => m.Id == k));

            // Page
            RegisterRepository(()
                => new Repository<Page, long>(
                    dbSet: databaseContext.Page,
                    modelKeyFunc: (m, k) => m.Id == k));

            // PageGroup
            RegisterRepository(()
                => new Repository<PageGroup, long>(
                    dbSet: databaseContext.PageGroup,
                    modelKeyFunc: (m, k) => m.Id == k));

            // Project
            RegisterRepository(()
                => new Repository<Project, long>(
                    dbSet: databaseContext.Project,
                    modelKeyFunc: (m, k) => m.Id == k));

            // Tag
            RegisterRepository(()
                => new Repository<Tag, string>(
                    dbSet: databaseContext.Tag,
                    modelKeyFunc: (m, k) => m.Name == k));

            // User
            RegisterRepository(()
                => new Repository<User, long>(
                    dbSet: databaseContext.User,
                    modelKeyFunc: (m, k) => m.Id == k));
        }
    }
}
