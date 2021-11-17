namespace AlinSpace.Database.Ef.Testing.Models
{
    public class Product : AbstractTenantEntity<long>
    {
        public string Description { get; set; }

        public double Price { get; set; }
    }
}
