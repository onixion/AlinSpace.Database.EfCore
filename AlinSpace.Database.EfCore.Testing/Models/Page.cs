namespace AlinSpace.Database.EfCore.Testing.Models
{
    public class Page : AbstractEntity
    {
        public Book Book { get; set; }
    }
}
