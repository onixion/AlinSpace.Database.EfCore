using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace AlinSpace.Database.Ef.Testing.Models
{
    public class Book : AbstractTenantEntity<long>
    {
        public ICollection<Page> Pages { get; set; }

        public override void OnModelCreating(ModelBuilder modelBuilder, Type entityType, string entityName = null)
        {
            base.OnModelCreating(modelBuilder, entityType, entityName);

            modelBuilder.Entity<Book>()
                .HasMany(x => x.Pages)
                .WithOne(x => x.Book)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
