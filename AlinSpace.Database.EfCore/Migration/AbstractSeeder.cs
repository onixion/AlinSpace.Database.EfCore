using System.Threading.Tasks;

namespace AlinSpace.Database.EfCore
{
    /// <summary>
    /// Abstract implementation for <see cref="ISeeder"/>.
    /// </summary>
    public abstract class AbstractSeeder : ISeeder
    {
        public virtual void Seed(ITransaction transaction)
        {
        }

        public virtual Task SeedAsync(ITransaction transaction)
        {
            return Task.CompletedTask;
        }
    }
}
