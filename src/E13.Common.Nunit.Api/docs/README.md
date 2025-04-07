# E13.Common.Nunit.Api

[![NuGet Version](https://img.shields.io/nuget/v/e13.common.nunit.api)](https://www.nuget.org/packages/E13.Common.Nunit.Api/)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

## Overview

E13.Common.Nunit.Api is a specialized package within the E13.Common collection designed for testing Web API projects with NUnit. It extends E13.Common.Nunit with API-specific testing utilities, including test server integration, HTTP client extensions, and authentication helpers.

## Features

- **API Test Fixtures**: Base classes for API testing with TestServer integration
- **Launch Settings Integration**: Automatic loading of launchSettings.json for test configuration
- **HTTP Client Extensions**: Helper methods for working with HttpClient in tests
- **Azure AD Authentication**: Utilities for authenticating with Azure AD in tests
- **Test Environment Management**: Tools for managing test environments and configurations

## Installation

```shell
dotnet add package E13.Common.Nunit.Api
```

## Usage

### Creating an API Test Fixture

```csharp
using E13.Common.Nunit.Api;
using Microsoft.AspNetCore.Hosting;
using NUnit.Framework;
using System.Net.Http;

// Create a test fixture for your API
public class MyApiTests : BaseApiFixture
{
    public MyApiTests() 
        : base("MyApi.Project", () => new WebHostBuilder().UseStartup<TestStartup>())
    {
    }
    
    [Test]
    public async Task GetEndpoint_ReturnsSuccessStatusCode()
    {
        // Get the configured HttpClient
        var client = GetHttpClient();
        
        // Make a request to your API
        var response = await client.GetAsync("/api/values");
        
        // Assert the response
        response.EnsureSuccessStatusCode();
    }
}
```

### Using Authentication in Tests

```csharp
using E13.Common.Nunit.Api;
using NUnit.Framework;
using System.Net.Http;

public class AuthenticatedApiTests
{
    private HttpClient _client;
    
    [SetUp]
    public void Setup()
    {
        _client = new HttpClient();
    }
    
    [Test]
    public async Task SecureEndpoint_WithAuthentication_ReturnsSuccessStatusCode()
    {
        // Authenticate with Azure AD using environment variables
        _client.TokenForAAD_TestEnabled(new[] { "api://my-api/access" });
        
        // Make a request to a secure endpoint
        var response = await _client.GetAsync("https://my-api.example.com/secure");
        
        // Assert the response
        response.EnsureSuccessStatusCode();
    }
}
```

### Required Environment Variables

For Azure AD authentication tests, set these environment variables in your launchSettings.json:

```json
{
  "profiles": {
    "MyTests": {
      "commandName": "Project",
      "environmentVariables": {
        "TokenForAAD_PublicClientId": "your-client-id",
        "TokenForAAD_TenantId": "your-tenant-id",
        "TokenForAAD_TestUser": "test.user@example.com",
        "TokenForAAD_TestPass": "your-test-password"
      }
    }
  }
}
```

## Dependencies

- .NET 9.0
- Microsoft.AspNetCore.TestHost
- Microsoft.Extensions.Hosting
- PuppeteerSharp (for UI testing integration)
- E13.Common.Api
- E13.Common.Nunit

## Related Packages

E13.Common.Nunit.Api is part of the E13.Common collection, which includes:

- E13.Common.Core - Core utilities and base classes
- E13.Common.Nunit - Base NUnit testing utilities
- E13.Common.Nunit.UI - NUnit utilities for UI testing
- E13.Common.Api - Web API layer components
- E13.Common.AspNet - ASP.NET shared components

## Contributing

Contributions to E13.Common.Nunit.Api are welcome. If you have suggestions or improvements, please submit an issue or create a pull request in the [GitHub repository](https://github.com/e13tech/common).

## License

This project is licensed under the MIT License. For more details, see the LICENSE file in the repository.
