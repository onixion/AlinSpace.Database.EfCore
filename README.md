<img src="https://github.com/onixion/AlinSpace.Database/blob/main/Assets/Icon.png" width="200" height="200">

# AlinSpace.Database

is a **Database abstraction layer**. It helps to simplify the process of working with data through EntityFrameworkCore.
In order to keep the library simple, some decisions have been made:

- **long** is used for **all primary keys**. Use hash IDs to map the primary key to strings, or add additional fields.

# Examples

 ```csharp
 // Prepare database context options.
 var options = new DbContextOptionsBuilder<MyDbContext>().Options
 
// Create the transaction object.
var transaction = TransactionFactory.Create<MyDbContext>(options);

// Get repository.
var userRepository = transaction.GetRepository<User>();

// Create entity.
userRepository.Create(...);
// Get entity.
userRepository.Get(...);
// Update entity.
userRepository.Update(...);
// Delete entity.
userRepository.Delete(...);

// Find entity.
userRepository.Find(...);
// Find many entities.
userRepository.FindMany(...);

// Create custom query.
var query = userRepository.Query;

// Commit changes.
transaction.Commit();
```
