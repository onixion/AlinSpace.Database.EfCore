using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace AlinSpace.Database.EfCore.Testing.Models
{
    public class Page : AbstractEntity
    {
        public Book OwnedByBook { get; set; }
        public long OwnedByBookId { get; set; }

        public IList<Chapter> Chapters { get; set; } = new List<Chapter>();

        protected override void CreateModel(ModelBuilder modelBuilder, Type entityType)
        {
            modelBuilder.Entity<Page>()
                .HasMany(x => x.Chapters)
                .WithOne(x => x.OwnedByPage)
                .HasForeignKey(x => x.OwnedByPageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
