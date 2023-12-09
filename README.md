# TVMaze API Integration - ASP.NET Web API Application

## Overview

This project is an ASP.NET Web API application designed to integrate with the TVMaze API ([TVMaze API](http://www.tvmaze.com/api)). The goal is to store and expose TV show information, following best practices in .NET development, for this I used the Clean Architecture and CQRS pattern with MediatR. The .NET version used is .NET 8, please note if you need to download the runtime package to run the application.

## Architecture

### Clean Architecture

The project follows the Clean Architecture principles, separating concerns into layers:

- **Application**: Contains application-specific business logic, use cases, and command/query handlers. The [MediatR](https://github.com/jbogard/MediatR) library is employed to implement the CQRS pattern effectively.

- **Domain**: Represents the core domain entities. It is independent of any infrastructure concerns.

- **Infrastructure**: Houses implementations of external concerns like external API integration.

- **Persistence**: Focuses on data storage and retrieval mechanisms, such as [Entity Framework](https://docs.microsoft.com/en-us/ef/) for database access.

- **Shared Kernel**: Contains shared code and utilities used across different layers to promote reusability.

- **Web API**: Serves as the entry point for communication with clients. Handles HTTP requests and orchestrates the application's behavior.

### CQRS Pattern

The [Command Query Responsibility Segregation (CQRS)](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs) pattern is applied to separate the command (write) and query (read) responsibilities. This promotes a cleaner and more maintainable codebase.

### MediatR

[MediatR](https://github.com/jbogard/MediatR) is utilized to implement the Mediator pattern, providing a simple and consistent way to send queries and commands between application components. This enhances the decoupling of different layers and promotes maintainability.

## Additional Features

- **Health Check Service**: A health check endpoint is implemented to monitor the application's health. It can be accessed at `/health`.

- **Global Exception Handler**: A global exception handler is in place to handle errors gracefully. It centralizes the handling of exceptions, providing consistent error responses.

## Usage

To run the solution, follow these steps:

1. Clone the repository to your local machine.
2. Open the solution in your preferred IDE (Visual Studio, VS Code, etc.). 
3. Set up the database connection string in the `appsettings.Development.json` file.
4. Build the app and place your preferred Command Interface in the following directory:

         ...\TvMaze.Api\bin\Debug\net8.0>
5. Run the Application using the following command:

        dotnet TvMaze.Service.Api.dll --environment development
6. For request the Application endpoints you can execute them from <b><i>TvMaze.Service.Api.http</i></b> file

## API Endpoints

- **Query Show Data from TvMaze Api Directly**: This is an endpoint that allows you to request the data of a show form the TvMaze Api. Example: `GET http://localhost:5000/shows/20`. Where `20` is the "<b>id</b>" of the show in the TvMaze Api's database.

- **Post the tv show's data in the Application database**: Endpoint secured with an API key to retrieve information of a show form the TvMaze Api and store it in the application database. Example: `POST http://localhost:5000/api/shows/20/create`. Where `20` is the "<b>id</b>" of the show in the TvMaze Api database. This endpoint returns the **guid**(`649871e3-e0c0-4822-af0b-685194e39de6`) of the show that was created in the Application database.

- **Query Show Data from the Application database**: This is an endpoint that allows you to request the data of a tv show form the Application database.<br>
  Example: `GET http://localhost:5000/shows/649871e3-e0c0-4822-af0b-685194e39de6`. Where `649871e3-e0c0-4822-af0b-685194e39de6` is the "<b>guid</b>" of the show in the Application database that was returned in the previous endpoint.

## Unit Tests

Unit tests are included in the project to ensure the reliability of the implemented feature. Unit tests were implemented to test the "Create TV Show" use case/functionality through respective command handler in the application layer. Unit test methods:

    1. Handle_Should_ReturnSuccessResult_WhenTheShowNotExistsInDb
    2. Handle_Should_ReturnFailureResult_WhenTheShowAlreadyExistsInDb
    3. Handle_Should_NotCallUnitOfWork_WhenShowAlreadyExistsInDb

## Conclusion

This project demonstrates a scalable and maintainable architecture following .NET best practices. The use of Clean Architecture, CQRS, and MediatR contributes to a well-organized and modular codebase, making it easy to extend and maintain in the future.
