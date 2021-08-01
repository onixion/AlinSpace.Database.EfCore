using System;

namespace AlinSpace.Database.Ef.Testing.Models
{
    public class Person
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public Order Order { get; set; }
    }
}
