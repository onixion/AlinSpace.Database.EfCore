using Microsoft.EntityFrameworkCore;
using System;

namespace AlinSpace.Database.Ef
{
    /// <summary>
    /// Represents an implementation of <see cref="AbstractRepository{TModel, TKey}"/> for EF.
    /// </summary>
    /// <typeparam name="TModel">Type of the model.</typeparam>
    /// <typeparam name="TKey">Type of the key.</typeparam>
    public class Repository<TModel, TKey> : AbstractRepository<TModel, TKey> where TModel : class
    {
        private readonly DbSet<TModel> dbSet;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbSet">DB set.</param>
        /// <param name="modelKeyFunc">Model key func.</param>
        public Repository(DbSet<TModel> dbSet, Func<TModel, TKey, bool> modelKeyFunc) : base(dbSet.AsQueryable(), modelKeyFunc)
        {
            this.dbSet = dbSet ?? throw new ArgumentNullException(nameof(dbSet));
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public override void Create(TModel model)
        {
            dbSet.Add(model);
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public override void Update(TModel model)
        {
            dbSet.Update(model);
        }

        /// <summary>
        /// Deletes the specified model with the given key.
        /// </summary>
        /// <param name="model">The model.</param>
        public override void Delete(TModel model)
        {
            dbSet.Remove(model);
        }
    }
}
