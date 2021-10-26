using AlinSpace.Database.Models;

namespace AlinSpace.Database.Ef.Testing.Models
{
    public class Product : AbstractTenantEntity
    {
        public string Description { get; set; }

        public double Price { get; set; }
    }
}
