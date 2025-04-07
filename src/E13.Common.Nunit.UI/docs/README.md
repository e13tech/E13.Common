# E13.Common.Nunit.UI

[![NuGet Version](https://img.shields.io/nuget/v/e13.common.nunit.ui)](https://www.nuget.org/packages/E13.Common.Nunit.UI/)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

## Overview

E13.Common.Nunit.UI is a specialized package within the E13.Common collection designed for UI testing with NUnit and PuppeteerSharp. It extends E13.Common.Nunit with UI-specific testing utilities, including browser automation, screenshot comparison, and authentication helpers for web applications.

## Features

- **UI Test Fixtures**: Base classes for UI testing with PuppeteerSharp integration
- **Screenshot Comparison**: Automated visual regression testing with screenshot comparison
- **Authentication Support**: Helpers for testing authenticated UI flows
- **Browser Automation**: Utilities for interacting with web pages in tests
- **Test Organization**: Attributes and extensions for organizing UI tests

## Installation

```shell
dotnet add package E13.Common.Nunit.UI
```

## Usage

### Creating a UI Test Fixture

```csharp
using E13.Common.Nunit.UI;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using PuppeteerSharp;
using System.Threading.Tasks;

// Create a test fixture for your UI
public class MyUITests : BaseUIFixture
{
    // Define viewport dimensions
    public MyUITests() 
        : base(1280, 720, () => CreateHostBuilder())
    {
    }
    
    private static IHostBuilder CreateHostBuilder() =>
        Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<TestStartup>();
            });
    
    [Test]
    public async Task HomePage_LoadsCorrectly()
    {
        // Navigate to a page
        await Page.GoToAsync("https://localhost:5001");
        
        // Take and compare a screenshot
        await Page.ConfirmScreenshot("HomePage");
        
        // Assert page content
        var title = await Page.GetTitleAsync();
        Assert.AreEqual("Home Page", title);
    }
}
```

### Testing Authenticated UI Flows

```csharp
using E13.Common.Nunit.UI;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using System.Threading.Tasks;

public class AuthenticatedUITests : BaseAuthUIFixture
{
    protected override string AuthUrl => "https://localhost:5001/login";
    
    public AuthenticatedUITests() 
        : base(1280, 720, () => CreateHostBuilder())
    {
    }
    
    private static IHostBuilder CreateHostBuilder() => 
        Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<TestStartup>();
            });
    
    [Test]
    [RequiresAuth]
    public async Task SecurePage_WhenAuthenticated_ShowsContent()
    {
        // The [RequiresAuth] attribute will trigger automatic login
        // Navigate to a secure page
        await Page.GoToAsync("https://localhost:5001/secure");
        
        // Take and compare a screenshot
        await Page.ConfirmScreenshot("SecurePage");
        
        // Assert secure content is visible
        var content = await Page.QuerySelectorAsync(".secure-content");
        Assert.IsNotNull(content);
    }
}
```

## Dependencies

- .NET 9.0
- PuppeteerSharp
- PuppeteerSharp.Contrib.Extensions
- PuppeteerSharp.Contrib.Should
- Microsoft.Extensions.Hosting
- E13.Common.Nunit

## Related Packages

E13.Common.Nunit.UI is part of the E13.Common collection, which includes:

- E13.Common.Core - Core utilities and base classes
- E13.Common.Nunit - Base NUnit testing utilities
- E13.Common.Nunit.Api - NUnit utilities for API testing
- E13.Common.Blazor - Blazor UI components
- E13.Common.AspNet - ASP.NET shared components

## Contributing

Contributions to E13.Common.Nunit.UI are welcome. If you have suggestions or improvements, please submit an issue or create a pull request in the [GitHub repository](https://github.com/e13tech/common).

## License

This project is licensed under the MIT License. For more details, see the LICENSE file in the repository.
