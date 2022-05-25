using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace AlinSpace.Database.EfCore.Testing.Models
{
    public class Book : AbstractEntity
    {
        public IList<Page> Pages { get; set; } = new List<Page>();

        public string Name { get; set; }

        protected override void CreateModel(ModelBuilder modelBuilder, Type entityType)
        {
            modelBuilder.Entity<Book>()
                .HasMany(x => x.Pages)
                .WithOne(x => x.OwnedByBook)
                .HasForeignKey(x => x.OwnedByBookId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
