# PixelPlanner

## Getting started

Navigate to the root of the repo and using the command line, run the following command:

```bash
docker compose up -d
```

It should start two containers - one for the database and one for the web app. The root of the web app (although noting is served form there) will be available at [http://localhost:81](http://localhost:81). A swagger UI will be available at [http://localhost:81/swagger](http://localhost:81/swagger).

## Architecture

The application is following Clean Architecture guidelines and is split into 4 layers:

- Entities: Contains the domain model: `Grid` entity, `Rectangle` value object and a generic `Result<T>` which is a basic implementation of the `OperationResult` pattern.
- UseCases: Contains the operations that are available to the user. It contains the logic of the application. They orchestrate the interactions between the entities and their logic, the persistence layer and the result to the client code.
- Persistence: Contains the logic for persisting the data using repositories. Currently it's implemented using an in memory collection. It's planned to be replaced with a MongoDB database.
- API: Contains the web API. It's a thin layer that only handles the HTTP requests and responses. It's using the `UseCases` to perform the operations. Also set's up dependency  injection to conform to the Clean Architecture guidelines.

## Testing

There are comprehensive unit tests for the `Entities` layers.

## Patterns and Principles used

### DDD

In addition to Clean Architecture, DDD principles are also used. In particular the Entities layer contains an `AggregateRoot` which is the `Grid` entity. The `Rectangle` value object is immutable and is used inside the `Grid` entity. The `Grid` entity also contains the logic for the adding and removing rectangles to and from the grid validating that the operations are possible given the current state of the grid.

### Always valid Domain Model

There is a school of though that advocates for having the domain model always in a valid state. This is achieved by having the domain model validate the operations that are performed on it and not allowing invalid operations to be performed. This is the approach that is used in this application. For example, the `Grid` entity validates that the rectangles that are added to it are not overlapping with other rectangles that are already in the grid. Normally this is implemented encapsulating the inner properties of the aggregate and only allowing the methods of it to change it's state. It shouldn't be possible to create invalid entities since constructors should validate the input arguments and throw exceptions if they are invalid.

In this particular project instead of throwing exceptions a `Result<T>` is returned. This is a generic class that can be used to return the result of an operation or an error message that might have occurred. This is a pattern that is used in functional programming languages. It's a way to handle errors without using exceptions. It's a bit more verbose than using exceptions but it's more explicit and it's easier to reason about the code. It also allows for the next pattern.

## Thin controllers

The `GridController` on the API layer only passes parameters to the use cases and act on the `Result<T>` that is returned. They don't contain any logic. The controller is using pattern matching as in functional programming to detect if the result is successful or not and return the corresponding HTTP status code according to ASP.NET conventions.
Controllers and DTOs don't contain any validation logic either on any parameter. By having an always valid domain model that returns a `Result<T>` it's not necessary to have duplicity on the validation of request parameters as they are handled by the Use Cases and Entities. Any validation error is encapsulated in the `Result<T>` and returned to the controller which then returns the corresponding HTTP status code.
