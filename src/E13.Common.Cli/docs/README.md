# E13.Common.Cli

[![NuGet Version](https://img.shields.io/nuget/v/e13.common.cli)](https://www.nuget.org/packages/E13.Common.Cli/)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

## Overview

E13.Common.Cli is a package within the E13.Common collection designed for building command-line interface (CLI) applications. It provides a foundation for creating consistent, user-friendly command-line tools with standardized options, logging, and interactive features.

## Features

- **Base CLI Options**: Standard command-line options for consistent user experience
- **Interactive Console**: Support for both interactive and non-interactive console modes
- **Fancy Console Output**: Customizable console headers and styling
- **Spinner Animation**: Built-in loading spinner for long-running operations
- **Logging Integration**: Standardized logging with verbosity controls
- **CommandLineParser Integration**: Pre-configured command-line argument parsing

## Installation

```shell
dotnet add package E13.Common.Cli
```

## Usage

### Creating a CLI Application

```csharp
using CommandLine;
using E13.Common.Cli;
using E13.Cli;
using Microsoft.Extensions.Logging;

// Define your command options by inheriting from BaseOptions
public class MyOptions : BaseOptions
{
    [Option('n', "name", Required = true, HelpText = "Name to greet")]
    public string Name { get; set; }
}

// Create your CLI program
public class Program
{
    public static void Main(string[] args)
    {
        // Set up logging
        var loggerFactory = LoggerFactory.Create(builder => 
            builder.AddConsole());
        var logger = loggerFactory.CreateLogger<Program>();
        
        // Create a console interface
        using var console = new E13ConsoleSimple(logger);
        
        // Parse command line arguments
        Parser.Default.ParseArguments<MyOptions>(args)
            .WithParsed(options => 
            {
                // Execute your command logic
                console.WriteLine($"Hello, {options.Name}!");
                console.Success("Command completed successfully");
            });
    }
}
```

### Available Console Types

- **E13ConsoleSimple**: Basic console with simple ASCII header
- **E13FancyConsole**: Enhanced console with colorful ASCII art header
- **E13ConsoleBase**: Abstract base class for creating custom console implementations

## Dependencies

- .NET 9.0
- CommandLineParser
- Microsoft.Extensions.Logging.Console
- E13.Common.Core

## Related Packages

E13.Common.Cli is part of the E13.Common collection, which includes:

- E13.Common.Core - Core utilities and base classes
- E13.Common.Api - Web API layer components
- E13.Common.AspNet - ASP.NET shared components
- E13.Common.Blazor - Blazor UI components
- E13.Common.Domain - Domain layer components

## Contributing

Contributions to E13.Common.Cli are welcome. If you have suggestions or improvements, please submit an issue or create a pull request in the [GitHub repository](https://github.com/e13tech/common).

## License

This project is licensed under the MIT License. For more details, see the LICENSE file in the repository.
