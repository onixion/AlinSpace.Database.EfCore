namespace AlinSpace.Database.Ef.Testing.Models
{
    public class Page : AbstractTenantEntity<long>
    {
        public Book Book { get; set; }
    }
}
