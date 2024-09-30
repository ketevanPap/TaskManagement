Architecture Overview
The application follows a layered Clean Architecture with well-separated responsibilities for business logic, database interactions, and presentation (API). The primary layers are:

Core: Contains the business logic, domain services, and the main abstractions used throughout the application.
Infrastructure: Implements the core abstractions, providing access to external resources like databases, repositories, and other services.
Presentation: Responsible for exposing the API and handling HTTP requests, including controllers, routing, and model binding.
Architectural Patterns Used
1. CQRS (Command Query Responsibility Segregation)

Purpose: This pattern is used to segregate read (query) and write (command) operations. Commands mutate state, while queries retrieve data without altering the system.

Implementation: MediatR is used to implement this pattern, where each command and query is handled by its respective handler, ensuring clean separation of responsibilities.

Example:

Commands: CreateTaskCommand, UpdateTaskCommand
Queries: GetAllTasksQuery, GetTaskByIdQuery
2. Repository Pattern

Purpose: Encapsulates the logic for accessing the database. It hides the specifics of the data source from the business logic, promoting the Dependency Inversion Principle.

Implementation: The IRepository interface defines CRUD operations, and specific implementations like TaskItemRepository interact with the DbContext.

Example:

Repositories: TaskItemRepository, UserRepository
3. Unit of Work Pattern

Purpose: To ensure that all database operations related to a particular transaction are executed together and either succeed or fail as a unit.
Implementation: The UnitOfWork class coordinates the repositories and manages the lifecycle of the transaction.
4. JWT-based Authentication

Purpose: To secure the API and restrict access to authorized users only.
Implementation: JWT tokens are generated during login and passed in the Authorization header for subsequent requests to protected endpoints.
5. Object-Relational Mapping (ORM)

Purpose: To interact with the SQL Server database using strongly-typed C# classes instead of raw SQL.
Implementation: Entity Framework Core is used as the ORM, allowing developers to define models and perform CRUD operations.
Folder Structure
TaskManagement.Core:

ApplicationService: Contains the command/query handlers, DTOs, and services.
DomainService: Contains domain abstractions (repositories, services).
Commands/Queries: Segregated based on the CQRS pattern.
TaskManagement.Entity:

Models: Represents the core entities of the application, such as TaskItem, ApplicationUser, and enums.
TaskManagement.Infrastructure:

Repositories: Implements the repositories that interact with the database using EF Core.
DatabaseContext: Contains the EF Core DbContext for managing the database connection and entity configurations.
Presentation:

Controllers: Exposes the API endpoints for interacting with tasks and users. This layer is responsible for handling HTTP requests and responses.
Security
JWT Token Authentication:

Purpose: JWT tokens are issued upon user login. These tokens are passed in the Authorization header to access protected resources. Token validation is handled using ASP.NET Core's JwtBearer middleware.
