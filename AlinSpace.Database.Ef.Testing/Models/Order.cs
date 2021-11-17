using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace AlinSpace.Database.Ef.Testing.Models
{
    public class Order : AbstractTenantEntity<long>
    {
        public Person Owner { get; set; }

        public OrderState State { get; set; }

        public IList<Product> Products { get; set; } = new List<Product>();

        public override void OnModelCreating(ModelBuilder modelBuilder, Type entityType)
        {
            base.OnModelCreating(modelBuilder, entityType);

            modelBuilder.Entity<Order>()
                .HasMany(x => x.Products);
        }
    }
}
