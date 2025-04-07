# E13.Common.Api.AzureAD

[![NuGet Version](https://img.shields.io/nuget/v/e13.common.api.azuread)](https://www.nuget.org/packages/E13.Common.Api.AzureAD/)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

## Overview

E13.Common.Api.AzureAD is a specialized package within the E13.Common collection designed to simplify Azure Active Directory integration for Web API projects. It extends the E13.Common.Api package with Azure AD authentication capabilities, making it easier to secure APIs with Microsoft Identity Platform.

## Features

- **Azure AD Authentication**: Pre-configured Microsoft Identity Web integration
- **JWT Bearer Configuration**: Properly configured token validation for Azure AD v2.0 endpoints
- **Multi-tenant Support**: Built-in support for multi-tenant applications
- **Swagger Integration**: Security definitions for Swagger/OpenAPI documentation
- **Session Management**: Distributed memory cache and session configuration

## Installation

```shell
dotnet add package E13.Common.Api.AzureAD
```

## Usage

### Configuring Azure AD Authentication

```csharp
// In Startup.cs or Program.cs
public void ConfigureServices(IServiceCollection services)
{
    // Configure Azure AD authentication with Swagger integration
    services.ConfigureAzureADAuth(
        Configuration, 
        apiVersion: "v1", 
        apiTitle: "My Secured API");
        
    // Add other services...
    services.AddStandardApi();
}
```

### Required Configuration

Your `appsettings.json` should include the following Azure AD configuration:

```json
{
  "AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "TenantId": "your-tenant-id",
    "ClientId": "your-client-id",
    "ClientSecret": "your-client-secret"
  }
}
```

## Dependencies

- .NET 9.0
- Microsoft.AspNetCore.App
- Microsoft.Identity.Web
- E13.Common.Api

## Related Packages

E13.Common.Api.AzureAD is part of the E13.Common collection, which includes:

- E13.Common.Core - Core utilities and base classes
- E13.Common.Api - Web API layer components
- E13.Common.AspNet - ASP.NET shared components
- E13.Common.Data.Db - Database access components
- E13.Common.Domain - Domain layer components
- E13.Common.RestEase - REST client utilities

## Contributing

Contributions to E13.Common.Api.AzureAD are welcome. If you have suggestions or improvements, please submit an issue or create a pull request in the [GitHub repository](https://github.com/e13tech/common).

## License

This project is licensed under the MIT License. For more details, see the LICENSE file in the repository.
