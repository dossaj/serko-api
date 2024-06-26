# serko-api

## Overview

Project is completely cross platform and setup to run in Visual Studio or Visual Studio Code.

Three launch tasks include

- Console / Web
- IIS Express (Windows Only)
- Docker (Setup to run on linux container)

Project utilizes the following libraries

- ASP.NET Core 8
- Fluent Validation
- Castle Windsor
- XUnit
- Log4Net
- NSwag
- Entity Framework
- SpecFlow

## Project Structure

The project is broken up into three main categories

- Server and Domain logic
- Boilerplate code not specific to this project, and normally doesn't need to be written per API
- Unit and Integration tests with integration tests using BDD.

## Getting Started

The project should run straight without any setup, other than the requirements for each launch task.

### Database

The database is using the entity framework so any connection should work. Setup is controlled by the following line in the `Program.cs`

```csharp
services.AddDbContext<ExpenseContext>(o => o.UseInMemoryDatabase("Expense"));
```

The database can be seeded from configuration. There is no need for this configuration and it may remain empty, as any `Vendor` that does not exist will be created on `POST`. This is only here to allow for migrations and example.

### Authentication

Authentication has been added to the API, it is configured to use an JWT access token. The access token can be generated at [jwt.io](https://jwt.io).

The token can be generated by using the `secret` configured in the `appsettings.json` file. By default the configuration is setup as

```js
  "Security": {
    "JwtKey": "A9AECC6C3D29F26F7C9977447AB94111"
  }
```

The token validation has been limited due to this being a demo.

### Configuration

The configuration gets pulled in through the IoC container and objects can take a dependencies directly from the configuration file.

Since the default database is in memory the `Connection` section of the configuration has been left empty.

The database seed configuration has been covered in the database section.

## Architecture

The project is layered into the following layers

- Web Layer
- Service Layer (Facade for domain API)
- Business Layer (Commands and Queries)
- DataAccess Layer (Provided by EF)

## Additional Notes

The web layer has content negotiation for the email being posted to the API. This is specified by the `Content-Type: application/email`.

Transactions are scoped per web request.

There is the addition of a `Postman` collection with some sample requests. These can be found in the `tools/postman` folder.
