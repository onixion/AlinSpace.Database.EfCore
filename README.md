<img src="https://github.com/onixion/AlinSpace.Database/blob/main/Assets/Icon.png" width="200" height="200">

# AlinSpace.Database
AlinSpace database repository and transaction interfaces.

# Basic usage

```csharp
// Create implementation of a transaction.
using var transaction = GetTransaction();

// Retrieve user repository.
var userRepository = transaction.GetRepository<User, long>();

// Retrieve a user.
var myUser = userRepository.Query().Where(u => u.Username == "MyUser").First();
```

# AlinSpace.Database.Ef
Implementation for Microsoft.EntityFrameworkCore.

# Basic usage with Entity-Framework

First create a database context:

```csharp
// Define a database context.
public class MyDatabaseContext : DbContext
{
    public DbSet<User> User { get; set; }

    public MyDatabaseContext(DbContextOptions<MyDatabaseContext> options) : base(options)
    {
    }
}
```

Then create a repository registry and transaction:

```csharp
// Create a repository registry.
var repositoryRegistry = new RepositoryRegistryBuilder()
    .Register(() => new Repository<User, long>(databaseContext, databaseContext.User))
    .Build();

// Create a transaction.
var transaction = new Transaction(databaseContext, repositoryRegistry);
```
