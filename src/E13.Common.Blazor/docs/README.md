# E13.Common.Blazor

[![NuGet Version](https://img.shields.io/nuget/v/e13.common.blazor)](https://www.nuget.org/packages/E13.Common.Blazor/)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

## Overview

E13.Common.Blazor is a specialized package within the E13.Common collection designed for building Blazor applications. It provides standardized components, extensions, and utilities to streamline the development of Blazor front-end projects in a layered application architecture.

## Features

- **Standard Blazor Configuration**: Extension methods for configuring Blazor services with best practices
- **Authentication Integration**: Pre-configured Microsoft Identity Web integration for Blazor Server
- **Application Builder Extensions**: Simplified setup for Blazor application middleware
- **Security Configuration**: Built-in authorization policies for secure applications
- **Environment-Aware Setup**: Different configurations for development and production environments

## Installation

```shell
dotnet add package E13.Common.Blazor
```

## Usage

### Configuring Blazor Server Services

```csharp
// In Startup.cs or Program.cs
public void ConfigureServices(IServiceCollection services)
{
    // Add standard Blazor Server services with authentication
    services.AddStandardBlazorServerSide(Configuration);
    
    // Add other services...
}
```

### Configuring Blazor Application

```csharp
// In Startup.cs or Program.cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // Configure standard Blazor middleware
    app.UseStandardBlazor(env);
}
```

## Dependencies

- .NET 9.0
- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.Identity.Web
- E13.Common.AspNet

## Related Packages

E13.Common.Blazor is part of the E13.Common collection, which includes:

- E13.Common.Core - Core utilities and base classes
- E13.Common.AspNet - ASP.NET shared components
- E13.Common.Api - Web API layer components
- E13.Common.Api.AzureAD - Azure AD authentication for APIs
- E13.Common.Data.Db - Database access components
- E13.Common.Domain - Domain layer components

## Contributing

Contributions to E13.Common.Blazor are welcome. If you have suggestions or improvements, please submit an issue or create a pull request in the [GitHub repository](https://github.com/e13tech/common).

## License

This project is licensed under the MIT License. For more details, see the LICENSE file in the repository.
