using Microsoft.EntityFrameworkCore;

namespace AlinSpace.Database.Ef
{
    /// <summary>
    /// Represents the abstract tenant db context.
    /// </summary>
    public abstract class AbstractTenantDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets the tenant database set.
        /// </summary>
        public DbSet<Tenant> Tenant { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options">Database context options.</param>
        public AbstractTenantDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new Tenant().OnModelCreating(modelBuilder, typeof(Tenant));
        }
    }
}
