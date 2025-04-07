# E13.Common.Data.Db

[![NuGet Version](https://img.shields.io/nuget/v/e13.common.data.db)](https://www.nuget.org/packages/E13.Common.Data.Db/)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

## Overview

E13.Common.Data.Db is a package within the E13.Common collection designed for database access using Entity Framework Core. It provides a foundation for implementing the data access layer in a layered application architecture, with built-in support for common database operations and entity tracking.

## Features

- **Base DbContext**: Extended DbContext with automatic entity tracking
- **Repository Pattern**: Generic repository implementation for consistent data access
- **Entity Tracking**: Automatic tracking of entity creation, modification, and deletion
- **Soft Delete**: Built-in support for soft delete functionality
- **Design-Time Factory**: Support for EF Core migrations and design-time context creation
- **Query Filters**: Global query filters for entity types

## Installation

```shell
dotnet add package E13.Common.Data.Db
```

## Usage

### Creating a Database Context

```csharp
using E13.Common.Data.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class MyDbContext : BaseDbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();
    
    public MyDbContext(DbContextOptions options, ILogger<MyDbContext> logger)
        : base(options, logger)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure your entity mappings
        
        base.OnModelCreating(modelBuilder);
    }
}
```

### Using the Repository Pattern

```csharp
// Define a repository for a specific entity
public class CustomerRepository : Repository<MyDbContext, Customer>
{
    public CustomerRepository(MyDbContext dbContext) : base(dbContext)
    {
    }
    
    // Add custom repository methods
    public IEnumerable<Customer> GetPremiumCustomers()
    {
        return GetAll(c => c.IsPremium);
    }
}

// Using the repository
var repository = new CustomerRepository(dbContext);
var customer = new Customer { Name = "John Doe" };
repository.Insert(customer);
dbContext.SaveChanges("admin", "CustomerService");
```

### Design-Time Factory for Migrations

```csharp
public class MyDbContextFactory : DesignTimeDbContextFactory<MyDbContext>
{
    // The base class implementation will use the DESIGN_CONTEXT environment variable
    // for the connection string when running migrations
}
```

## Database Migrations

To work with migrations:

1. Set the connection string environment variable:
   ```powershell
   $env:DESIGN_CONTEXT = "Server=127.0.0.1;Database=MyDb;User Id=sa;Password=P@ssword!;"
   ```

2. Create a migration:
   ```powershell
   dotnet ef migrations add InitialCreate --project MyProject
   ```

3. Update the database:
   ```powershell
   dotnet ef database update --project MyProject
   ```

## Dependencies

- .NET 9.0
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.Extensions.Logging

## Related Packages

E13.Common.Data.Db is part of the E13.Common collection, which includes:

- E13.Common.Core - Core utilities and base classes
- E13.Common.Domain - Domain layer components
- E13.Common.Api - Web API layer components
- E13.Common.AspNet - ASP.NET shared components
- E13.Common.Blazor - Blazor UI components

## Contributing

Contributions to E13.Common.Data.Db are welcome. If you have suggestions or improvements, please submit an issue or create a pull request in the [GitHub repository](https://github.com/e13tech/common).

## License

This project is licensed under the MIT License. For more details, see the LICENSE file in the repository.
