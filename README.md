What is the .NET HacksPack?
=====================
A smart set of common classes and implementations distrubuted as little packages to improve your development productivity.

### Why HacksPack?
- .NET HacksPack is being developed in a way to transform it self and your project in a structured "Plugable application", where you can detach any party from HacksPack/project and replace it by a custom implementation, or another .NET HacksPack that provide the desiered tools.
- Yes you read correctly, as I sad you can also replace part of the .NET HacksPack implemantation. It is helpfull when part of the solution is not satisfing your project's goals but you wont implement the inteire comunication with a queue e.g
- And as a "Plugable Application" you can change your backstage without any impact for you business logic. "Hack" your project ;D changing the .NET HacksPack implementation.

## Give a Star! :star:
If you liked the project or if NetHacksPack helped you, please give a star ;)

## Get Started
.NET HacksPack can be installed using the Nuget package manager or the `dotnet` CLI.

### .NET NetHacksPack.Core
---
Here we have the core structures used by the Packages
```
dotnet add package NetHacksPack.Core
```
---

| Package |  Version | Popularity |
| ------- | ----- | ----- |
| `NetHacksPack.Core` | [![NuGet](https://img.shields.io/nuget/v/NetHacksPack.Core.svg)](https://nuget.org/packages/NetHacksPack.Core) | [![Nuget](https://img.shields.io/nuget/dt/NetHacksPack.Core.svg)](https://nuget.org/packages/NetHacksPack.Core.svg) |


### .NET NetHacksPack.Hosting.Abstractions
---
This package contains definitions for abstractions that can be usefull to create background hosts (also know as windows|linux - services).

See the [official documentation](https://github.com/EdneySilva/NetHacksPack/blob/develop/src/NetHacksPack.Hosting.Abstractions/README.md)
```
dotnet add package NetHacksPack.Hosting.Abstractions
```
---

| Package |  Version | Popularity |
| ------- | ----- | ----- |
| `NetHacksPack.Hosting.Abstractions` | [![NuGet](https://img.shields.io/nuget/v/NetHacksPack.Hosting.Abstractions.svg)](https://nuget.org/packages/NetHacksPack.Hosting.Abstractions) | [![Nuget](https://img.shields.io/nuget/dt/NetHacksPack.Hosting.Abstractions.svg)](https://nuget.org/packages/NetHacksPack.Core.svg) |


### .NET NetHacksPack.Hosting
---
This package is a stantard implementation from the package .NetHacksPack.Hosting.Abstractionos to help build a standart host application

See the [official documentation](https://github.com/EdneySilva/NetHacksPack/blob/develop/src/NetHacksPack.Hosting/README.md)
```
dotnet add package NetHacksPack.Hosting
```
---

| Package |  Version | Popularity |
| ------- | ----- | ----- |
| `NetHacksPack.Hosting` | [![NuGet](https://img.shields.io/nuget/v/NetHacksPack.Hosting.svg)](https://nuget.org/packages/NetHacksPack.Hosting) | [![Nuget](https://img.shields.io/nuget/dt/NetHacksPack.Hosting.svg)](https://nuget.org/packages/NetHacksPack.Hosting.svg) |


### .NET NetHacksPack.Hosting.Connections
---
This package is a stantard implementation from the package .NetHacksPack.Hosting.Abstractionos that provides a IConnectionProvider implementation, help you get connection information

See the [official documentation](https://github.com/EdneySilva/NetHacksPack/blob/develop/src/NetHacksPack.Hosting.Connections/README.md)
```
dotnet add package NetHacksPack.Hosting.Connections
```
---

| Package |  Version | Popularity |
| ------- | ----- | ----- |
| `NetHacksPack.Hosting.Connections` | [![NuGet](https://img.shields.io/nuget/v/NetHacksPack.Hosting.Connections.svg)](https://nuget.org/packages/NetHacksPack.Hosting.Connections) | [![Nuget](https://img.shields.io/nuget/dt/NetHacksPack.Hosting.Connections.svg)](https://nuget.org/packages/NetHacksPack.Hosting.Connections.svg) |


### .NET NetHacksPack.Hosting.Environment
---
n/a
```
dotnet add package NetHacksPack.Hosting.Environment
```
---

| Package |  Version | Popularity |
| ------- | ----- | ----- |
| `NetHacksPack.Hosting.Environment` | [![NuGet](https://img.shields.io/nuget/v/NetHacksPack.Hosting.Environment.svg)](https://nuget.org/packages/NetHacksPack.Hosting.Environment) | [![Nuget](https://img.shields.io/nuget/dt/NetHacksPack.Hosting.Environment.svg)](https://nuget.org/packages/NetHacksPack.Hosting.Environment.svg) |


### .NET NetHacksPack.Hosting.Listener
---
n/a
```
dotnet add package NetHacksPack.Hosting.Listener
```
---

| Package |  Version | Popularity |
| ------- | ----- | ----- |
| `NetHacksPack.Hosting.Listener` | [![NuGet](https://img.shields.io/nuget/v/NetHacksPack.Hosting.Listener.svg)](https://nuget.org/packages/NetHacksPack.Hosting.Listener) | [![Nuget](https://img.shields.io/nuget/dt/NetHacksPack.Hosting.Listener.svg)](https://nuget.org/packages/NetHacksPack.Hosting.Listener.svg) |


### .NET NetHacksPack.Hosting.Web.IIS
---
n/a
```
dotnet add package NetHacksPack.Hosting.Web.IIS
```
---

| Package |  Version | Popularity |
| ------- | ----- | ----- |
| `NetHacksPack.Hosting.Web.IIS` | [![NuGet](https://img.shields.io/nuget/v/NetHacksPack.Hosting.Web.IIS.svg)](https://nuget.org/packages/NetHacksPack.Hosting.Web.IIS) | [![Nuget](https://img.shields.io/nuget/dt/NetHacksPack.Hosting.Web.IIS.svg)](https://nuget.org/packages/NetHacksPack.Hosting.Web.IIS.svg) |


### .NET NetHacksPack.Integration.Abstractions
---
n/a
```
dotnet add package NetHacksPack.Integration.Abstractions
```
---

| Package |  Version | Popularity |
| ------- | ----- | ----- |
| `NetHacksPack.Integration.Abstractions` | [![NuGet](https://img.shields.io/nuget/v/NetHacksPack.Integration.Abstractions.svg)](https://nuget.org/packages/) | [![Nuget](https://img.shields.io/nuget/dt/NetHacksPack.Integration.Abstractions.svg)](https://nuget.org/packages/NetHacksPack.Integration.Abstractions.svg) |


### .NET NetHacksPack.Integration.RabbitMQ
---
n/a
```
dotnet add package NetHacksPack.Integration.RabbitMQ
```
---

| Package |  Version | Popularity |
| ------- | ----- | ----- |
| `NetHacksPack.Integration.RabbitMQ` | [![NuGet](https://img.shields.io/nuget/v/NetHacksPack.Integration.RabbitMQ.svg)](https://nuget.org/packages/NetHacksPack.Integration.RabbitMQ) | [![Nuget](https://img.shields.io/nuget/dt/NetHacksPack.Integration.RabbitMQ.svg)](https://nuget.org/packages/NetHacksPack.Integration.RabbitMQ.svg) |


### .NET NetHacksPack.Integration.RabbitMQ.Handlers
---
n/a
```
dotnet add package NetHacksPack.Integration.RabbitMQ.Handlers
```
---

| Package |  Version | Popularity |
| ------- | ----- | ----- |
| `NetHacksPack.Integration.RabbitMQ.Handlers` | [![NuGet](https://img.shields.io/nuget/v/NetHacksPack.Integration.RabbitMQ.Handlers.svg)](https://nuget.org/packages/NetHacksPack.Integration.RabbitMQ.Handlers) | [![Nuget](https://img.shields.io/nuget/dt/NetHacksPack.Integration.RabbitMQ.Handlers.svg)](https://nuget.org/packages/NetHacksPack.Integration.RabbitMQ.Handlers.svg) |


### .NET NetHacksPack.Data.Persistence.Abstractions
---
n/a
```
dotnet add package NetHacksPack.Data.Persistence.Abstractions
```
---

| Package |  Version | Popularity |
| ------- | ----- | ----- |
| `NetHacksPack.Data.Persistence.Abstractions` | [![NuGet](https://img.shields.io/nuget/v/NetHacksPack.Data.Persistence.Abstractions.svg)](https://nuget.org/packages/NetHacksPack.Data.Persistence.Abstractions) | [![Nuget](https://img.shields.io/nuget/dt/NetHacksPack.Data.Persistence.Abstractions.svg)](https://nuget.org/packages/NetHacksPack.Data.Persistence.Abstractions.svg) |


### .NET NetHacksPack.Data.Persistence.EF
---
n/a
```
dotnet add package NetHacksPack.Data.Persistence.EF
```
---

| Package |  Version | Popularity |
| ------- | ----- | ----- |
| `NetHacksPack.Data.Persistence.EF` | [![NuGet](https://img.shields.io/nuget/v/NetHacksPack.Data.Persistence.EF.svg)](https://nuget.org/packages/NetHacksPack.Data.Persistence.EF) | [![Nuget](https://img.shields.io/nuget/dt/NetHacksPack.Data.Persistence.EF.svg)](https://nuget.org/packages/NetHacksPack.Data.Persistence.EF.svg) |


### .NET NetHacksPack.ClientApi.Abstractions
---
Will be available soon
```
n/a
```
---

| Package |  Version | Popularity |
| ------- | ----- | ----- |
| n/a| n/a| n/a|


### .NET NetHacksPack.ClientApi
---
Will be available soon
```
n/a
```
---

| Package |  Version | Popularity |
| ------- | ----- | ----- |
| n/a| n/a| n/a|


### .NET NetHacksPack.ClientApi.Providers.Json
---
Will be available soon
```
n/a
```
---

| Package |  Version | Popularity |
| ------- | ----- | ----- |
| n/a| n/a| n/a|


### Examples
- The sample application inside from project is in development and is not ready to use but will be available soon. In the meantime, explore the code to understand what it covers. 

## About
.NET HacksPack was developed by [Edney Batista da Silva](https://www.linkedin.com/in/edneybatistadasilva/) under the [MIT license](license).