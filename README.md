<img src="https://github.com/onixion/AlinSpace.Database/blob/main/Assets/Icon.png" width="200" height="200">

# AlinSpace.Database.EfCore

is a **Database abstraction layer**. It helps to simplify the process of working with data through *EntityFrameworkCore*.

In order to keep the library simple, some decisions have been made:
- **long** is used for **all primary keys**. Use hash IDs to map the primary key to strings, or add additional fields.

# Example

First create a database context like this:

```csharp
public class MyDatabaseContext : AbstractDbContext
{
    public DbSet<Book> Books { get; set; }

    public MyDatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
}
```
Then create the options and setup the transaction like this:

 ```csharp
// Prepare database context options.
var options = optionsBuilder.Build();
 
// Create the transaction object.
var transaction = TransactionFactory.Create<MyDatabaseContext>(options);

// Get repository.
var bookRepository = transaction.GetRepository<Book>();

// Create entity.
bookRepository.Create(...);
// Get entity.
bookRepository.Get(...);
// Update entity.
bookRepository.Update(...);
// Delete entity.
bookRepository.Delete(...);

// Find entity.
bookRepository.Find(...);
// Find many entities.
bookRepository.FindMany(...);

// Create a custom query.
var query = bookRepository.NewQuery();

// Commit changes.
transaction.Commit();
```
