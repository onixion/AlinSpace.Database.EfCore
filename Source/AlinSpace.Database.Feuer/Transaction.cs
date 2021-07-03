using AlinSpace.Database.Ef;
using AlinSpace.Database.Feuer.Models;
using System;

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
            RegisterRepository(() 
                => new Repository<User, long>(
                    dbSet: databaseContext.User,
                    modelKeyFunc: (m, k) => m.Id == k));

            RegisterRepository(()
                => new Repository<Page, long>(
                    dbSet: databaseContext.Page,
                    modelKeyFunc: (m, k) => m.Id == k));
        }
    }
}
