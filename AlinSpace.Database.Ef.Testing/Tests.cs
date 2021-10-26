using AlinSpace.Database.Models;
using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                var factory = TransactionFactoryBuilder<DatabaseContext>
                    .New()
                    .WithRegistry((c, b) =>
                    {
                        b.RegisterTenantRepository(c);
                        b.Register(() => new Repository<Models.Order>(c, c.Order));
                        b.Register(() => new Repository<Models.Person>(c, c.Person));
                        b.Register(() => new Repository<Models.Product>(c, c.Product));
                    })
                    .WithTransaction((c, r) =>  new Transaction(c, r))
                    .Build();
               
                var dbContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                    .UseSqlite(DatabaseManager.ConnectionString)
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                    .EnableServiceProviderCaching()
                    .Options;

                var databaseContext = new DatabaseContext(dbContextOptions);

                using (var transaction = factory.CreateTransaction(databaseContext))
                {
                    var tenantRepository = transaction.GetRepository<Database.Models.Tenant>();
                    var orderRepository = transaction.GetRepository<Models.Order>();
                    var personRepository = transaction.GetRepository<Models.Person>();
                    var productRepository = transaction.GetRepository<Models.Product>();

                    #region Generate products

                    var productFaker = new Faker<Models.Product>()
                            .RuleFor(o => o.Name, (f, p) => f.Name.Random.Words(2))
                            .RuleFor(o => o.Description, (f, p) => f.Lorem.Random.Words(20));

                    var products = productFaker.Generate(1).ToArray();
                    
                    foreach (var product in products)
                    {
                        product.Tenant = null;

                        productRepository.Add(product);
                    }

                    transaction.Commit();

                    #endregion

                    #region Generate orders

                    var orderFaker = new Faker<Models.Order>()
                            .RuleFor(o => o.State, (f, p) => f.Random.Enum<Models.OrderState>())
                            .RuleFor(o => o.Products, (f, p) => products
                                .OrderBy(p => f.Random.Double())
                                .Take(f.Random.Int(0, 100))
                                .ToList());

                    #endregion

                    var personFaker = new Faker<Models.Person>()
                            .RuleFor(p => p.FirstName, (f, p) => f.Person.FirstName)
                            .RuleFor(p => p.LastName, (f, p) => f.Person.LastName)
                            .RuleFor(p => p.BirthDate, (f, p) => f.Date.Past())
                            .RuleFor(p => p.Orders, (f, p) => orderFaker.Generate(2).ToArray());

                    foreach (var _ in Enumerable.Range(1, 10))
                    {
                        personRepository.Add(personFaker.Generate());
                    }

                    transaction.Commit();

                    var persons = personRepository
                        .NewQuery()
                        .Include(p => p.Orders)
                        .ToList();

                    foreach(var person in persons)
                    {
                        Console.WriteLine($"Person: {person.FirstName} {person.LastName}");

                        foreach(var order in person.Orders)
                        {
                            Console.WriteLine($"  - Order: {order.Products.Count} product(s)");
                        }
                    } 

                    Console.ReadKey();
                }
            }
            catch(Exception e)
            {
                throw;
            }
            finally
            {
                DatabaseManager.Delete();
            }
        }

        [Fact]
        public void Test2()
        {
            DatabaseManager.Delete();
            DatabaseManager.Create();

            try
            {
                var factory = TransactionFactoryBuilder<DatabaseContext>
                    .New()
                    .WithRegistry((c, b) =>
                    {
                        b.RegisterTenantRepository(c);
                        b.Register(() => new Repository<Models.Order>(c, c.Order));
                        b.Register(() => new Repository<Models.Person>(c, c.Person));
                        b.Register(() => new Repository<Models.Product>(c, c.Product));
                    })
                    .WithTransaction((c, r) => new Transaction(c, r))
                    .Build();

                var dbContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                    .UseSqlite(DatabaseManager.ConnectionString)
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                    .EnableServiceProviderCaching()
                    .Options;

                var databaseContext = new DatabaseContext(dbContextOptions);

                using (var transaction = factory.CreateTransaction(databaseContext))
                {
                    var tenantRepository = transaction.GetRepository<Database.Models.Tenant>();
                    var orderRepository = transaction.GetRepository<Models.Order>();
                    var personRepository = transaction.GetRepository<Models.Person>();
                    var productRepository = transaction.GetRepository<Models.Product>();

                    var tenants = new List<Tenant>()
                    {
                        new Tenant()
                        {
                            Name = "Red",
                        },
                        new Tenant()
                        {
                            Name = "Blue",
                        }
                    };

                    foreach (var tenant in tenants)
                    {
                        #region Generate products

                        var productFaker = new Faker<Models.Product>()
                                .RuleFor(o => o.Name, (f, p) => f.Name.Random.Words(2))
                                .RuleFor(o => o.Description, (f, p) => f.Lorem.Random.Words(20));

                        var products = productFaker.Generate(1).ToArray();

                        foreach (var product in products)
                        {
                            product.Tenant = tenant;
                            productRepository.Add(product);
                        }

                        transaction.Commit();

                        #endregion

                        #region Generate orders

                        var orderFaker = new Faker<Models.Order>()
                                .RuleFor(o => o.State, (f, p) => f.Random.Enum<Models.OrderState>())
                                .RuleFor(o => o.Products, (f, p) => products
                                    .OrderBy(p => f.Random.Double())
                                    .Take(f.Random.Int(0, 100))
                                    .ToList());

                        #endregion

                        var personFaker = new Faker<Models.Person>()
                                .RuleFor(p => p.FirstName, (f, p) => f.Person.FirstName)
                                .RuleFor(p => p.LastName, (f, p) => f.Person.LastName)
                                .RuleFor(p => p.BirthDate, (f, p) => f.Date.Past())
                                .RuleFor(p => p.Orders, (f, p) => orderFaker.Generate(2).Select(x => { x.Tenant = tenant; return x; }).ToArray());

                        var persons = personFaker.Generate(10).ToArray();

                        foreach (var person in persons)
                        {
                            person.Tenant = tenant;
                            personRepository.Add(person);
                        }

                        transaction.Commit();
                    }

                    var personsFromTenant1 = personRepository
                        .NewQuery()
                        .ScopeTenant(1)
                        .ToList();

                    var personsFromTenant2 = personRepository
                       .NewQuery()
                       .ScopeTenant(2)
                       .ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                DatabaseManager.Delete();
            }
        }
    }
}
