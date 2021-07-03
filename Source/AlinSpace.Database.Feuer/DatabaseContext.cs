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
        /// Gets or sets the db set for <see cref="Models.Configuration"/>.
        /// </summary>
        public DbSet<Configuration> Configuration { get; set; }

        /// <summary>
        /// Gets or sets the db set for <see cref="Models.Page"/>.
        /// </summary>
        public DbSet<Page> Page { get; set; }

        /// <summary>
        /// Gets or sets the db set for <see cref="Models.PageGroup"/>.
        /// </summary>
        public DbSet<PageGroup> PageGroup { get; set; }

        /// <summary>
        /// Gets or sets the db set for <see cref="Models.Project"/>.
        /// </summary>
        public DbSet<Project> Project { get; set; }

        /// <summary>
        /// Gets or sets the db set for <see cref="Models.Tag"/>.
        /// </summary>
        public DbSet<Tag> Tag { get; set; }

        /// <summary>
        /// Gets or sets the db set for <see cref="Models.User"/>.
        /// </summary>
        public DbSet<User> User { get; set; }

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
            modelBuilder.Entity<Configuration>().HasOne(u => u.IndexPage);
            modelBuilder.Entity<Configuration>().HasOne(u => u.ContactPage);
            modelBuilder.Entity<Configuration>().HasOne(u => u.AboutPage);

            modelBuilder.Entity<Page>().HasOne(p => p.Owner);
            modelBuilder.Entity<Page>().HasMany(p => p.Tags);

            modelBuilder.Entity<PageGroup>().HasOne(p => p.Owner);
            modelBuilder.Entity<PageGroup>().HasMany(p => p.Pages);
            modelBuilder.Entity<PageGroup>().HasMany(p => p.Tags);

            modelBuilder.Entity<Project>().HasOne(p => p.Owner);
            modelBuilder.Entity<Project>().HasOne(p => p.Page);

            modelBuilder.Entity<User>().HasMany(u => u.Pages);
            modelBuilder.Entity<User>().HasMany(u => u.PageGroups);
            modelBuilder.Entity<User>().HasMany(u => u.Projects);
        }
    }
}
