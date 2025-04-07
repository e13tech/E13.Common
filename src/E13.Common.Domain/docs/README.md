# E13.Common.Domain

[![NuGet Version](https://img.shields.io/nuget/v/e13.common.domain)](https://www.nuget.org/packages/E13.Common.Domain/)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

## Overview

E13.Common.Domain is a package within the E13.Common collection that provides core domain abstractions and patterns for building domain-driven applications. It defines interfaces and base classes for domain entities and implements the specification pattern for encapsulating business rules.

## Features

- **Entity Interfaces**: Core interfaces for domain entities (IEntity, ICreatable, IModifiable, IDeletable)
- **Specification Pattern**: Implementation of the specification pattern for business rules
- **Composable Specifications**: Support for combining specifications using logical operators (And, Or, Not)
- **Entity Lifecycle**: Interfaces for tracking entity lifecycle (creation, modification, deletion)
- **Expiration Support**: Interface for entities with expiration dates

## Installation

```shell
dotnet add package E13.Common.Domain
```

## Usage

### Creating Domain Entities

```csharp
using E13.Common.Domain;

// Basic entity
public class Product : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// Entity with tracking information
public class Customer : IEntity, ICreatable, IModifiable, IDeletable
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    // ICreatable implementation
    public string? CreatedBy { get; set; }
    public string? CreatedSource { get; set; }
    public DateTime? Created { get; set; }
    
    // IModifiable implementation
    public string? ModifiedBy { get; set; }
    public string? ModifiedSource { get; set; }
    public DateTime? Modified { get; set; }
    
    // IDeletable implementation
    public string? DeletedBy { get; set; }
    public string? DeletedSource { get; set; }
    public DateTime? Deleted { get; set; }
    
    // IDeletable provides this method automatically
    // public bool IsDeleted() => Deleted != null;
}
```

### Using the Specification Pattern

```csharp
using E13.Common.Domain.Specifications;

// Create a specification for active customers
public class ActiveCustomerSpecification : Specification<Customer>
{
    public override Expression<Func<Customer?, bool>> ToExpression()
    {
        return customer => customer != null && customer.Deleted == null;
    }
}

// Create a specification for premium customers
public class PremiumCustomerSpecification : Specification<Customer>
{
    public override Expression<Func<Customer?, bool>> ToExpression()
    {
        return customer => customer != null && customer.IsPremium;
    }
}

// Using specifications
var activeSpec = new ActiveCustomerSpecification();
var premiumSpec = new PremiumCustomerSpecification();

// Combine specifications
var activePremiumSpec = activeSpec.BitwiseAnd(premiumSpec);

// Check if a customer satisfies the specification
bool isPremiumActive = activePremiumSpec.IsSatisfiedBy(customer);

// Use with LINQ
var activePremiumCustomers = dbContext.Customers
    .Where(activePremiumSpec.ToExpression())
    .ToList();
```

## Dependencies

- .NET 9.0

## Related Packages

E13.Common.Domain is part of the E13.Common collection, which includes:

- E13.Common.Core - Core utilities and base classes
- E13.Common.Data.Db - Database access components
- E13.Common.Api - Web API layer components
- E13.Common.AspNet - ASP.NET shared components
- E13.Common.Logic - Business logic components

## Contributing

Contributions to E13.Common.Domain are welcome. If you have suggestions or improvements, please submit an issue or create a pull request in the [GitHub repository](https://github.com/e13tech/common).

## License

This project is licensed under the MIT License. For more details, see the LICENSE file in the repository.
