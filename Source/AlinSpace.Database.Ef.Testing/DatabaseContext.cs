using AlinSpace.Database.Ef.Testing.Models;
using Microsoft.EntityFrameworkCore;

namespace AlinSpace.Database.Ef.Testing
{
    /// <summary>
    /// Test database context.
    /// </summary>
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// Gets or sets the orders.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Gets or sets the persons.
        /// </summary>
        public DbSet<Person> Persons { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}
