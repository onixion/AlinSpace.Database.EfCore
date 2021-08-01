using Bogus;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace AlinSpace.Database.Ef.Testing
{
    public class Tests
    {
        [Fact]
        public void Test()
        {
            DatabaseManager.Delete();
            DatabaseManager.Create();

            try
            {
                using (var transaction = TransactionFactory.CreateTransaction(true))
                {
                    var orderRepository = transaction.GetRepository<Models.Order, long>();
                    var personRepository = transaction.GetRepository<Models.Person, long>();
                    var productRepository = transaction.GetRepository<Models.Product, long>();

                    // Generate fake products

                    var productFaker = new Faker<Models.Product>()
                            .RuleFor(o => o.Name, (f, p) => f.Name.Random.Words(2))
                            .RuleFor(o => o.Name, (f, p) => f.Lorem.Random.Words(20));

                    var products = productFaker.Generate(2000).ToArray();

                    foreach(var product in products)
                        productRepository.Add(product);

                    transaction.Commit();

                    var orderFaker = new Faker<Models.Order>()
                            .RuleFor(o => o.State, (f, p) => f.Random.Enum<Models.OrderState>())
                            .RuleFor(o => o.Products, (f, p) => products
                                .OrderBy(p => f.Random.Double())
                                .Take(f.Random.Int(0, 100))
                                .ToList());

                    var personFaker = new Faker<Models.Person>()
                            .RuleFor(p => p.FirstName, (f, p) => f.Person.FirstName)
                            .RuleFor(p => p.LastName, (f, p) => f.Person.LastName)
                            .RuleFor(p => p.BirthDate, (f, p) => f.Date.Past())
                            .RuleFor(p => p.Order, (f, p) => orderFaker.Generate());

                    foreach (var _ in Enumerable.Range(1, 1000))
                    {
                        var generatedPerson = personFaker.Generate();
                        personRepository.Add(generatedPerson);
                    }

                    transaction.Commit();

                    var persons = personRepository.NewQuery().Include(p => p.Order).ToList();
                }
            }
            finally
            {
                DatabaseManager.Delete();
            }
        }
    }
}
