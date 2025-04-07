# E13.Common.AspNet

[![NuGet Version](https://img.shields.io/nuget/v/e13.common.aspnet)](https://www.nuget.org/packages/E13.Common.AspNet/)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

## Overview

E13.Common.AspNet is a foundational package within the E13.Common collection that provides shared components and utilities for ASP.NET Core-based applications. It serves as a common base for both API and UI projects, offering functionality that can be leveraged across different types of web applications.

## Features

- **ASP.NET Core Integration**: Core components for ASP.NET Core applications
- **Newtonsoft.Json Support**: Pre-configured JSON serialization with Newtonsoft.Json
- **Environment Configuration**: Utilities for working with environment variables
- **Core Extensions**: Built on top of E13.Common.Core to provide web-specific functionality

## Installation

```shell
dotnet add package E13.Common.AspNet
```

## Usage

E13.Common.AspNet serves as a foundation for other more specialized packages in the E13.Common collection, such as E13.Common.Api and E13.Common.Blazor. While it can be used directly, it's typically referenced by these higher-level packages.

## Dependencies

- .NET 9.0
- Microsoft.AspNetCore.App
- Microsoft.AspNetCore.Mvc.NewtonsoftJson
- Microsoft.Extensions.Configuration.EnvironmentVariables
- E13.Common.Core

## Related Packages

E13.Common.AspNet is part of the E13.Common collection, which includes:

- E13.Common.Core - Core utilities and base classes
- E13.Common.Api - Web API layer components
- E13.Common.Api.AzureAD - Azure AD authentication for APIs
- E13.Common.Blazor - Blazor UI components
- E13.Common.Data.Db - Database access components
- E13.Common.Domain - Domain layer components

## Contributing

Contributions to E13.Common.AspNet are welcome. If you have suggestions or improvements, please submit an issue or create a pull request in the [GitHub repository](https://github.com/e13tech/common).

## License

This project is licensed under the MIT License. For more details, see the LICENSE file in the repository.
