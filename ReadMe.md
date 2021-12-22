# Project Structure
The project structure and design based on Domain Driven Design and Clean Architecture design pattern.
## Core.Application
## Core.Domain
The Domain layer contains businese logic, entities and types. It is an independent, other projects will flow inward and depend on this domain project.
Domain project should consist of POCO and should not depend on external library and frameworks includes .Net Framework.
* Entities
* Aggregates
* Value Objects
* Domain Events
* Constants and Enums
* Interfaces Contract

## Core.Infratruture
It is data layer project implements the Interfaces Contract repository to persist data to database.
It also provides other services like email and queue.
* EF, Data context
* Configuration
* Email service

**Ex. Migration and Update Database**
```
	> dotnet ef migrations add InitDataTables -c AppDbContext -s Web.WebApp -p Core.Infrastructure
	> dotnet ef database update -c AppDbContext -s Web.WebApp -p Core.Infrastructure
```

## Web.WebApp
Blazor web application is UI gives users, especially admin, to manage, control and perform businese as usual.



## Web.WebApi
Provides endpoints RESTFUL API/ GraphQL to mobile applications

# References:
- Clean Architecture Template, https://github.com/jasontaylordev/CleanArchitecture, viewed 10/12/2021
- Clear Architecture, https://medium.com/dotnet-hub/clean-architecture-with-dotnet-and-dotnet-core-aspnetcore-overview-introduction-getting-started-ec922e53bb97, viewed 10/12/2021
- Fluent Validation, https://medium.com/dotnet-hub/use-fluentvalidation-in-asp-net-or-asp-net-core-d0b891e5e87b, viewed 10/12/2021
