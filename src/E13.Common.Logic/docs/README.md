# E13.Common.Logic

[![NuGet Version](https://img.shields.io/nuget/v/e13.common.logic)](https://www.nuget.org/packages/E13.Common.Logic/)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

## Overview

E13.Common.Logic is a package within the E13.Common collection designed for implementing business logic that spans across multiple domains or data layers. It provides a foundation for encapsulating complex business rules and operations in a layered application architecture.

## Features

- **Logic Interface**: Core interface for implementing business logic components
- **Context Management**: Built-in context handling for managing resources
- **Cross-Domain Logic**: Support for implementing logic that spans multiple domain entities
- **Business Rule Encapsulation**: Structure for encapsulating complex business rules

## Installation

```shell
dotnet add package E13.Common.Logic
```

## Usage

### Creating a Logic Component

```csharp
using E13.Common.Logic;
using Microsoft.EntityFrameworkCore;

// Define a logic component for order processing
public class OrderProcessingLogic : ILogic<MyDbContext>
{
    public MyDbContext Context { get; }
    
    public OrderProcessingLogic(MyDbContext context)
    {
        Context = context;
    }
    
    public void ProcessOrder(Order order)
    {
        // Implement cross-domain business logic
        // For example, updating inventory, creating invoices, etc.
        
        // Access multiple repositories or entities through the context
        //...
        
        // Save changes
        Context.SaveChanges();
    }
}
```

### Using the Logic Component

```csharp
// In a service or controller
public class OrderService
{
    private readonly OrderProcessingLogic _orderLogic;
    
    public OrderService(OrderProcessingLogic orderLogic)
    {
        _orderLogic = orderLogic;
    }
    
    public void PlaceOrder(Order order)
    {
        // Use the logic component to process the order
        _orderLogic.ProcessOrder(order);
    }
}
```

## Dependencies

- .NET 9.0

## Related Packages

E13.Common.Logic is part of the E13.Common collection, which includes:

- E13.Common.Core - Core utilities and base classes
- E13.Common.Domain - Domain layer components
- E13.Common.Data.Db - Database access components
- E13.Common.Api - Web API layer components
- E13.Common.AspNet - ASP.NET shared components

## Contributing

Contributions to E13.Common.Logic are welcome. If you have suggestions or improvements, please submit an issue or create a pull request in the [GitHub repository](https://github.com/e13tech/common).

## License

This project is licensed under the MIT License. For more details, see the LICENSE file in the repository.
