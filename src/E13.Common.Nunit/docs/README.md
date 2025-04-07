# E13.Common.Nunit

[![NuGet Version](https://img.shields.io/nuget/v/e13.common.nunit)](https://www.nuget.org/packages/E13.Common.Nunit/)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

## Overview

E13.Common.Nunit is a package within the E13.Common collection designed to enhance NUnit testing capabilities. It provides custom attributes, extensions, and utilities to streamline test development and improve test organization for projects using the NUnit testing framework.

## Features

- **Custom Test Attributes**: Specialized attributes for categorizing and controlling test execution
  - `IntegrationAttribute`: For marking integration tests that should be skipped in in-memory mode
  - `RegressionAttribute`: For categorizing regression tests
  - `SmokeAttribute`: For categorizing smoke tests
- **Environment Handling**: Utilities for managing environment variables during tests
- **String Extensions**: Helper methods for working with paths and directories in tests
- **FluentAssertions Integration**: Pre-configured integration with FluentAssertions

## Installation

```shell
dotnet add package E13.Common.Nunit
```

## Usage

### Using Custom Test Attributes

```csharp
using E13.Common.Nunit;
using NUnit.Framework;

public class MyTests
{
    [Test]
    [Smoke]
    public void BasicFunctionality_ShouldWork()
    {
        // This test will be categorized as a smoke test
        // Useful for running only smoke tests: dotnet test --filter Category=Smoke
    }
    
    [Test]
    [Regression]
    public void FixedBug_ShouldNotReoccur()
    {
        // This test will be categorized as a regression test
        // Useful for running only regression tests: dotnet test --filter Category=Regression
    }
    
    [Test]
    [Integration]
    public void DatabaseConnection_ShouldWork()
    {
        // This test will be skipped if running in in-memory mode
        // Useful for tests that require external resources
    }
}
```

### Using String Extensions

```csharp
using System;
using E13.Common.Nunit;
using NUnit.Framework;

public class PathTests
{
    [Test]
    public void FindParentDirectory()
    {
        var currentDir = Directory.GetCurrentDirectory();
        
        // Find a parent directory by name
        var projectDir = currentDir.ParentDirectory("MyProject");
        
        // Use the project directory for test file operations
        var testFilePath = Path.Combine(projectDir, "TestData", "sample.json");
    }
}
```

## Dependencies

- .NET 9.0
- NUnit
- FluentAssertions
- Newtonsoft.Json
- E13.Common.Core

## Related Packages

E13.Common.Nunit is part of the E13.Common collection, which includes:

- E13.Common.Core - Core utilities and base classes
- E13.Common.Nunit.Api - NUnit utilities for API testing
- E13.Common.Nunit.UI - NUnit utilities for UI testing
- E13.Common.Domain - Domain layer components
- E13.Common.Data.Db - Database access components

## Contributing

Contributions to E13.Common.Nunit are welcome. If you have suggestions or improvements, please submit an issue or create a pull request in the [GitHub repository](https://github.com/e13tech/common).

## License

This project is licensed under the MIT License. For more details, see the LICENSE file in the repository.
