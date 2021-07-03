using AlinSpace.Database.Feuer.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AlinSpace.Database.Feuer
{
    /// <summary>
    /// Represents the database context.
    /// </summary>
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// Gets or sets the db set for <see cref="Models.User"/>.
        /// </summary>
        public DbSet<User> User { get; set; }

        /// <summary>
        /// Gets or sets the db set for <see cref="Models.Page"/>.
        /// </summary>
        public DbSet<Page> Page { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options">Database context options.</param>
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// On model creating.
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Pages);

            modelBuilder
                .Entity<Page>()
                .HasOne(p => p.Owner);
        }
    }
}
