using AlinSpace.Database.EfCore.Testing.Models;
using Microsoft.EntityFrameworkCore;

namespace AlinSpace.Database.EfCore.Testing
{
    public class DatabaseContext : AbstractDbContext
    {
        public DbSet<Book> Book { get; set; }

        public DbSet<Page> Page { get; set; }

        public DbSet<Chapter> Chapter { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}
