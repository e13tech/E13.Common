# E13.Common.Api

[![NuGet Version](https://img.shields.io/nuget/v/e13.common.api)](https://www.nuget.org/packages/E13.Common.Api/)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

## Overview

E13.Common.Api is a package within the E13.Common collection designed for building Web API projects. It provides standardized components, extensions, and utilities to streamline the development of API layers in a layered application architecture.

## Features

- **Standard API Configuration**: Extension methods for configuring API services with best practices
- **Swagger Integration**: Pre-configured Swagger/OpenAPI documentation setup
- **CORS Configuration**: Built-in Cross-Origin Resource Sharing policy setup
- **Diagnostics Helpers**: Tools for exposing diagnostic information in development environments
- **Authentication Extensions**: Utilities for working with claims and authentication
- **Database Context Integration**: Simplified setup for Entity Framework database contexts

## Installation

```shell
dotnet add package E13.Common.Api
```

## Usage

### Adding Standard API Services

```csharp
// In Startup.cs or Program.cs
public void ConfigureServices(IServiceCollection services)
{
    // Add standard API services (controllers, CORS, JSON handling)
    services.AddStandardApi();
    
    // Add a database context with standard configuration
    services.AddStandardDbContext<MyDbContext>(Configuration, "DefaultConnection");
}
```

## Dependencies

- .NET 9.0
- Microsoft.AspNetCore.App
- Microsoft.ApplicationInsights.AspNetCore
- Microsoft.EntityFrameworkCore
- Swashbuckle.AspNetCore (Swagger)
- E13.Common.AspNet
- E13.Common.Data.Db

## Related Packages

E13.Common.Api is part of the E13.Common collection, which includes:

- E13.Common.Core - Core utilities and base classes
- E13.Common.AspNet - ASP.NET shared components
- E13.Common.Api.AzureAD - Azure AD authentication for APIs
- E13.Common.Data.Db - Database access components
- E13.Common.Domain - Domain layer components
- E13.Common.RestEase - REST client utilities

## Contributing

Contributions to E13.Common.Api are welcome. If you have suggestions or improvements, please submit an issue or create a pull request in the [GitHub repository](https://github.com/e13tech/common).

## License

This project is licensed under the MIT License. For more details, see the LICENSE file in the repository.
