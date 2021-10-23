using AlinSpace.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace AlinSpace.Database.Ef.Testing.Models
{
    public class Person : AbstractTenantEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public IList<Order> Orders { get; set; }

        public override void OnModelCreating(ModelBuilder modelBuilder, Type entityType)
        {
            base.OnModelCreating(modelBuilder, entityType);

            modelBuilder.Entity<Person>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.Owner);
        }
    }
}
