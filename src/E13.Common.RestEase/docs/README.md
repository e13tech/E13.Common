# E13.Common.RestEase

[![NuGet Version](https://img.shields.io/nuget/v/e13.common.restease)](https://www.nuget.org/packages/E13.Common.RestEase/)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

## Overview

E13.Common.RestEase is a package within the E13.Common collection designed to simplify API client development using the RestEase library. It provides base interfaces and utilities for creating strongly-typed API clients that can communicate with RESTful web services.

## Features

- **Base API Interfaces**: Core interfaces for creating RestEase API clients
- **Authentication Support**: Built-in support for authenticated API requests
- **Standard Headers**: Pre-configured headers for API requests
- **JSON Patch Integration**: Support for JSON Patch operations in API clients

## Installation

```shell
dotnet add package E13.Common.RestEase
```

## Usage

### Creating a Basic API Client

```csharp
using E13.Common.ApiClient;
using RestEase;
using System.Threading.Tasks;

// Define your API interface
public interface IMyApi : IRestEaseApi
{
    [Get("users/{userId}")]
    Task<User> GetUserAsync([Path] int userId);
    
    [Post("users")]
    Task<User> CreateUserAsync([Body] User user);
}

// Use the API client
public class MyService
{
    private readonly IMyApi _api;
    
    public MyService(string baseUrl)
    {
        _api = RestClient.For<IMyApi>(baseUrl);
    }
    
    public async Task<User> GetUserAsync(int userId)
    {
        return await _api.GetUserAsync(userId);
    }
}
```

### Creating an Authenticated API Client

```csharp
using E13.Common.ApiClient;
using RestEase;
using System.Net.Http.Headers;
using System.Threading.Tasks;

// Define your authenticated API interface
public interface IMySecureApi : IAuthenticatedApi
{
    [Get("secure/data")]
    Task<SecureData> GetSecureDataAsync();
}

// Use the authenticated API client
public class MySecureService
{
    private readonly IMySecureApi _api;
    
    public MySecureService(string baseUrl, string token)
    {
        _api = RestClient.For<IMySecureApi>(baseUrl);
        _api.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
    
    public async Task<SecureData> GetSecureDataAsync()
    {
        return await _api.GetSecureDataAsync();
    }
}
```

## Dependencies

- .NET 9.0
- RestEase
- Microsoft.AspNetCore.JsonPatch

## Related Packages

E13.Common.RestEase is part of the E13.Common collection, which includes:

- E13.Common.Core - Core utilities and base classes
- E13.Common.Api - Web API layer components
- E13.Common.AspNet - ASP.NET shared components
- E13.Common.Domain - Domain layer components
- E13.Common.Data.Db - Database access components

## Contributing

Contributions to E13.Common.RestEase are welcome. If you have suggestions or improvements, please submit an issue or create a pull request in the [GitHub repository](https://github.com/e13tech/common).

## License

This project is licensed under the MIT License. For more details, see the LICENSE file in the repository.
