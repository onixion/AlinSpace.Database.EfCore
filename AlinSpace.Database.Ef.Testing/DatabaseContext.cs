using AlinSpace.Database.Ef.Testing.Models;
using Microsoft.EntityFrameworkCore;

namespace AlinSpace.Database.Ef.Testing
{
    public class DatabaseContext : AbstractTenantDbContext
    {
        public DbSet<Book> Book { get; set; }

        public DbSet<Page> Page { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new Book().OnModelCreating(modelBuilder, typeof(Book));
            new Page().OnModelCreating(modelBuilder, typeof(Page));
        }
    }
}
