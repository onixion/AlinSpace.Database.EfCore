using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace AlinSpace.Database.EfCore
{
    /// <summary>
    /// Represents the seeders.
    /// </summary>
    public class Seeders
    {
        /// <summary>
        /// Seeder items.
        /// </summary>
        public IEnumerable<ISeeder> Items { get; private set; }

        /// <summary>
        /// Source assembly for seeders.
        /// </summary>
        public Assembly SourceAssembly { get; private set; }

        /// <summary>
        /// Private constructor.
        /// </summary>
        private Seeders()
        {
        }

        /// <summary>
        /// Create empty seeders.
        /// </summary>
        public static Seeders None()
        {
            return new Seeders();
        }

        /// <summary>
        /// Create seeders from enumerable of seeders.
        /// </summary>
        /// <param name="items">Seeders.</param>
        public static Seeders From(IEnumerable<ISeeder> items)
        {
            return new Seeders()
            {
                Items = new ReadOnlyCollection<ISeeder>(items?.ToList()),
            };
        }

        /// <summary>
        /// Create seeders from assembly.
        /// </summary>
        public static Seeders FromAssembly(Assembly assembly)
        {
            return new Seeders()
            {
                SourceAssembly = assembly,
            };
        }

        /// <summary>
        /// Create seeders from calling assembly.
        /// </summary>
        public static Seeders FromThisAssembly()
        {
            return FromAssembly(Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// Get seeders.
        /// </summary>
        /// <returns>Enumerable of seeders.</returns>
        public IEnumerable<ISeeder> GetSeeders()
        {
            if (Items != null && Items.Any())
            {
                return Items;
            }

            if (SourceAssembly != null)
            {
                return SourceAssembly
                    .GetTypes()
                    .Where(x => x.IsSubclassOf(typeof(ISeeder)))
                    .Select(x => (ISeeder)x)
                    .ToList();
            }

            return Enumerable.Empty<ISeeder>();
        }
    }
}
