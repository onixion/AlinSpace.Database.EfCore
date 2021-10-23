using AlinSpace.Database.Ef.Testing.Models;
using Microsoft.EntityFrameworkCore;

namespace AlinSpace.Database.Ef.Testing
{
    public class DatabaseContext : AbstractTenantDbContext
    {
        public DbSet<Order> Order { get; set; }

        public DbSet<Person> Person { get; set; }

        public DbSet<Product> Product { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new Order().OnModelCreating(modelBuilder, typeof(Order));
            new Person().OnModelCreating(modelBuilder, typeof(Person));
            new Product().OnModelCreating(modelBuilder, typeof(Product));
        }
    }
}
