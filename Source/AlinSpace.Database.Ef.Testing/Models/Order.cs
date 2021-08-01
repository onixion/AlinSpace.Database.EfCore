using System.Collections.Generic;

namespace AlinSpace.Database.Ef.Testing.Models
{
    public class Order
    {
        public long Id { get; set; }

        public OrderState State { get; set; }

        public IList<Product> Products { get; set; } = new List<Product>();
    }
}
