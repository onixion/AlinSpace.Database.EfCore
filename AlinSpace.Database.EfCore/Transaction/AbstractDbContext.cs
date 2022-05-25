using Microsoft.EntityFrameworkCore;
using System;

namespace AlinSpace.Database.EfCore
{
    public abstract class AbstractDbContext : DbContext
    {
        public AbstractDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entityTypes = EntityTypesCache.GetFromDbContextType(GetType());

            foreach(var entityType in entityTypes)
            {
                var entity = Activator.CreateInstance(entityType) as IEntity;
                entity.OnModelCreating(modelBuilder, entityType);
            }
        }
    }
}
