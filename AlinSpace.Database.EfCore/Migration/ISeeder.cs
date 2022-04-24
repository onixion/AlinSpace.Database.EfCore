using System.Threading.Tasks;

namespace AlinSpace.Database.EfCore
{
    /// <summary>
    /// Represents the seeder interface.
    /// </summary>
    public interface ISeeder
    {
        /// <summary>
        /// Seed.
        /// </summary>
        /// <param name="transaction">Transaction.</param>
        void Seed(ITransaction transaction);

        /// <summary>
        /// Seed asynchronously.
        /// </summary>
        /// <param name="transaction">Transaction.</param>
        Task SeedAsync(ITransaction transaction);
    }
}
