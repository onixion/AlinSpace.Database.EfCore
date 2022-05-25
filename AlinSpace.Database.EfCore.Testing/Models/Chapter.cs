namespace AlinSpace.Database.EfCore.Testing.Models
{
    public class Chapter : AbstractEntity
    {
        public Page OwnedByPage { get; set; }
        public long OwnedByPageId { get; set; }
    }
}
