using System;
using System.Collections.Generic;
using System.Linq;

namespace AlinSpace.Database
{
    /// <summary>
    /// Represents an abstract implementation of <see cref="IRepository{TModel,TKey}"/>.
    /// </summary>
    /// <typeparam name="TModel">Type of the model.</typeparam>
    public abstract class AbstractRepository<TModel, TKey> : IRepository<TModel, TKey> where TModel : class 
    {
        private readonly IQueryable<TModel> queryable;
        private readonly Func<TModel, TKey, bool> modelKeyFunc;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="queryable">Queryable.</param>
        /// <param name="modelKeyFunc">Model key func.</param>
        public AbstractRepository(
            IQueryable<TModel> queryable, 
            Func<TModel, TKey, bool> modelKeyFunc)
        {
            this.queryable = queryable ?? throw new ArgumentNullException(nameof(queryable));
            this.modelKeyFunc = modelKeyFunc ?? throw new ArgumentNullException(nameof(modelKeyFunc));
        }

        /// <summary>
        /// Checks if the key exists.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>True, if it exists; false otherwise.</returns>
        public bool Exists(TKey key)
        {
            return queryable.Any(m => modelKeyFunc(m, key));
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="func">Queryable func.</param>
        /// <returns>Model.</returns>
        public TModel Get(TKey key, Func<IQueryable<TModel>, IQueryable<TModel>> func = null)
        {
            var tmpQueryable = queryable.Where(m => modelKeyFunc(m, key));

            if (func != null)
                tmpQueryable = func(queryable);

            return queryable.FirstOrDefault();
        }

        /// <summary>
        /// Find a specific element.
        /// </summary>
        /// <param name="func">Queryable func.</param>
        /// <returns>Optional model.</returns>
        public TModel Find(Func<IQueryable<TModel>, IQueryable<TModel>> func = null)
        {
            var tmpQueryable = queryable;

            if (func != null)
                tmpQueryable = func(queryable);

            return tmpQueryable.FirstOrDefault();
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public abstract void Create(TModel model);

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public abstract void Update(TModel model);

        /// <summary>
        /// Deletes the specified model with the given key.
        /// </summary>
        /// <param name="model">The model.</param>
        public abstract void Delete(TModel model);

        /// <summary>
        /// Get count of elements.
        /// </summary>
        /// <param name="func">Queryable func.</param>
        /// <returns>Number of elements.</returns>
        public int GetCount(Func<IQueryable<TModel>, IQueryable<TModel>> func = null)
        {
            var tmpQueryable = queryable;

            if (func != null)
                tmpQueryable = func(tmpQueryable);

            return tmpQueryable.Count();
        }

        /// <summary>
        /// Get elements.
        /// </summary>
        /// <param name="skip">Number of items to skip.</param>
        /// <param name="take">Number of items to take.</param>
        /// <param name="func">Queryable func.</param>
        /// <returns>Enumerable of elements.</returns>
        public IEnumerable<TModel> Get(int skip = 0, int take = 20, Func<IQueryable<TModel>, IQueryable<TModel>> func = null)
        {
            var tmpQueryable = queryable;

            if (func != null)
                tmpQueryable = func(tmpQueryable);

            return tmpQueryable.Skip(skip).Take(take);
        }
    }
}
