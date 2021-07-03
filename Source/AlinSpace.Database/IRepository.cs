using AlinSpace.FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlinSpace.Database
{
    /// <summary>
    /// Represents the repository interface.
    /// </summary>
    /// <typeparam name="TModel">Type of the model.</typeparam>
    /// <typeparam name="TKey">Type of the key.</typeparam>
    public interface IRepository<TModel, TKey> where TModel : class
    {
        /// <summary>
        /// Checks if the key exists.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>True, if it exists; false otherwise.</returns>
        bool Exists(TKey key);

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="func">Queryable func.</param>
        /// <returns>Model.</returns>
        Optional<TModel> Get(TKey key, Func<IQueryable<TModel>, IQueryable<TModel>> func = null);

        /// <summary>
        /// Find a specific element.
        /// </summary>
        /// <param name="func">Queryable func.</param>
        /// <returns>Optional model.</returns>
        Optional<TModel> Find(Func<IQueryable<TModel>, IQueryable<TModel>> func = null);

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Create(TModel model);

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(TModel model);

        /// <summary>
        /// Deletes the specified model with the given key.
        /// </summary>
        /// <param name="model">The model.</param>
        void Delete(TModel model);

        /// <summary>
        /// Get count of elements.
        /// </summary>
        /// <param name="func">Queryable func.</param>
        /// <returns>Number of elements.</returns>
        int GetCount(Func<IQueryable<TModel>, IQueryable<TModel>> func = null);

        /// <summary>
        /// Get elements.
        /// </summary>
        /// <param name="skip">Number of items to skip.</param>
        /// <param name="take">Number of items to take.</param>
        /// <param name="func">Queryable func.</param>
        /// <returns>Enumerable of elements.</returns>
        IEnumerable<TModel> Get(int skip = 0, int take = 20, Func<IQueryable<TModel>, IQueryable<TModel>> func = null);
    }
}
