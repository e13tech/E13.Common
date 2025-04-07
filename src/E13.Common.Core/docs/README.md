# E13.Common.Core

[![NuGet Version](https://img.shields.io/nuget/v/e13.common.core)](https://www.nuget.org/packages/E13.Common.Core/)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

## Overview

E13.Common.Core is a foundational package within the E13.Common collection, providing essential utilities and base classes to support the development of applications following a layered architecture. This package offers core functionalities that can be leveraged across various project types to promote consistency and efficiency in application development.

## Features

- **Attribute Extensions**: Custom attributes like `DisplayAttribute`, `AbbrevationAttribute`, and `GuidAttribute` for enhanced enum functionality
- **Environment Utilities**: Tools for checking runtime environment settings
- **Extension Methods**: Helpful extensions for common types including:
  - Enum extensions for display and abbreviation handling
  - IQueryable extensions for pagination
  - String extensions for secure string conversion
- **Pagination Support**: Built-in support for paged collections
- **Polly Integration**: Resilience and transient-fault-handling with Polly

## Installation

```shell
dotnet add package E13.Common.Core
```

## Usage

After installation, include the necessary namespaces in your code to access the provided utilities and base classes:

```csharp
using E13.Common.Core;
using E13.Common.Core.Attributes;
```

### Example: Using Enum Extensions

```csharp
public enum Status
{
    [Display("Active")]
    [Abbrevation("A")]
    Active,

    [Display("Inactive")]
    [Abbrevation("I")]
    Inactive
}

// Usage
Status status = Status.Active;
string display = status.AsDisplay(); // Returns "Active"
string abbr = status.AsAbbreviation(); // Returns "A"
```

### Example: Environment Variables

```csharp
// Check if running in memory mode
bool inMemory = EnvironmentVars.IsRunningInMemory();
```

## Dependencies

- .NET 9.0
- Microsoft.Extensions.Logging.Abstractions
- Newtonsoft.Json
- MoreLinq
- Polly

## Related Packages

E13.Common.Core is part of the E13.Common collection, which includes:

- E13.Common.Api - Web API layer components
- E13.Common.AspNet - ASP.NET shared components
- E13.Common.Blazor - Blazor UI components
- E13.Common.Data.Db - Database access components
- E13.Common.Domain - Domain layer components
- E13.Common.RestEase - REST client utilities

## Contributing

Contributions to E13.Common.Core are welcome. If you have suggestions or improvements, please submit an issue or create a pull request in the [GitHub repository](https://github.com/e13tech/common).

## License

This project is licensed under the MIT License. For more details, see the LICENSE file in the repository.