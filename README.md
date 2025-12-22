# MongoDB Product Management API

A modern, scalable product management system built with .NET 10, MongoDB, and Clean Architecture principles. This API provides comprehensive product management capabilities with a NoSQL database backend optimized for flexible product data structures.

## Table of Contents
- [Overview](#overview)
- [Technology Stack](#technology-stack)
- [Architecture](#architecture)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [API Endpoints](#api-endpoints)
- [Running with Docker](#running-with-docker)
- [Development](#development)
- [Testing](#testing)
- [Contributing](#contributing)
- [License](#license)

## Overview

The MongoDB Product Management API is a robust, enterprise-grade solution designed to handle complex product data with the flexibility of MongoDB's document-based storage. Unlike traditional relational databases, MongoDB allows for dynamic product schemas, making it ideal for e-commerce platforms with diverse product types and attributes.

The system follows Clean Architecture and CQRS (Command Query Responsibility Segregation) patterns, providing a maintainable and testable codebase. It leverages MongoDB's powerful querying capabilities to efficiently manage product catalogs of any size.

## Technology Stack

- **Backend**: .NET 10
- **Database**: MongoDB (NoSQL Document Database)
- **Architecture**: Clean Architecture + CQRS
- **ORM**: MongoDB.Driver
- **API Framework**: ASP.NET Core Web API
- **Mediator**: MediatR
- **Documentation**: NSwag (Swagger/OpenAPI)
- **Dependency Injection**: Built-in .NET DI
- **Serialization**: System.Text.Json
- **Validation**: FluentValidation
- **Containerization**: Docker & Docker Compose
- **ORM Mapping**: AutoMapper
- **Logging**: Serilog

## Architecture

This project follows Clean Architecture principles with the following layers:

### Domain Layer
- Contains business entities, value objects, domain events, and domain exceptions
- Defines domain interfaces (repositories)
- Contains business rules and validation logic

### Application Layer
- Contains application logic and use cases
- Implements CQRS pattern with Commands and Queries
- Defines DTOs for API contracts
- Contains validation behaviors and mapping profiles

### Infrastructure Layer
- Implements domain interfaces (repository implementations)
- Handles data persistence with MongoDB
- Manages database contexts and configurations

### API Layer
- Contains controllers and API endpoints
- Handles HTTP requests and responses
- Implements middleware and extensions

## Features

### Core Product Management
- **Create Products**: Add new products with flexible attributes
- **Retrieve Products**: Get all products or specific product by ID
- **Update Products**: Modify existing product information
- **Flexible Schema**: Store diverse product attributes in document format

### MongoDB-Specific Capabilities
- **Document-Based Storage**: Store complex, nested product data structures
- **Flexible Schema**: Accommodate different product types without database migrations
- **Rich Query Language**: Leverage MongoDB's powerful query capabilities
- **Scalability**: Horizontal scaling with sharding support
- **Indexing**: Optimized performance with custom indexes

### API Features
- **RESTful API**: Standard HTTP methods and status codes
- **CQRS Pattern**: Separation of read and write operations
- **Auto-generated Documentation**: Interactive API documentation via Swagger
- **Validation**: Comprehensive input validation with FluentValidation
- **Error Handling**: Centralized exception handling middleware
- **Clean Architecture**: Maintainable and testable codebase

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (for containerized deployment)
- [MongoDB](https://www.mongodb.com/try/download/community) (if running locally without Docker)

## Installation

### Option 1: Local Development Setup

1. Clone the repository:
```bash
git clone <repository-url>
cd ProductManagement
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Update the MongoDB connection string in `appsettings.json` if needed:
```json
{
  "MongoDB": {
    "ConnectionString": "mongodb://admin:admin123@localhost:27017",
    "DatabaseName": "ProductManagementDB"
  }
}
```

4. Run the application:
```bash
dotnet run --project src/ProductManagement.Api
```

### Option 2: Docker Compose Setup

1. Clone the repository:
```bash
git clone <repository-url>
cd ProductManagement
```

2. Build and run with Docker Compose:
```bash
docker-compose up --build
```

## Configuration

### Database Configuration
The application uses MongoDB as its primary data store. Configure the connection in `appsettings.json`:

```json
{
  "MongoDB": {
    "ConnectionString": "mongodb://admin:admin123@mongodb:27017",
    "DatabaseName": "ProductManagementDB"
  }
}
```

### Environment Variables
The application can be configured using environment variables, especially useful in containerized environments:

- `MongoDB__ConnectionString`: MongoDB connection string
- `MongoDB__DatabaseName`: MongoDB database name
- `ASPNETCORE_ENVIRONMENT`: Application environment (Development, Staging, Production)

## API Endpoints

The API provides the following endpoints:

### Products

#### Get All Products
- **GET** `/api/products`
- Returns a list of all products in the system
- Response: `200 OK` with array of Product objects

#### Get Product by ID
- **GET** `/api/products/{id}`
- Returns a specific product by its ID
- Response: `200 OK` with Product object or `404 Not Found`

#### Create Product
- **POST** `/api/products`
- Creates a new product
- Request Body: Product creation data
- Response: `201 Created` with new product ID or `400 Bad Request`

#### Update Product
- **PUT** `/api/products/{id}`
- Updates an existing product
- Request Body: Product update data
- Response: `200 OK` on success or `400 Bad Request`/`404 Not Found`

### API Documentation
Interactive API documentation is available at:
- Swagger UI: `http://localhost:8080/swagger`
- OpenAPI JSON: `http://localhost:8080/swagger/v1/swagger.json`

## Running with Docker

The project includes Docker support for easy deployment and consistent environments:

### Docker Services
- `productmanagement.api`: Main API application
- `mongodb`: MongoDB database instance
- `mongo-express`: Web-based MongoDB administration tool

### Running the Stack
```bash
# Build and start all services
docker-compose up --build

# Run in detached mode
docker-compose up --build -d
```

### Accessing Services
- API: `http://localhost:8080`
- MongoDB Express Admin: `http://localhost:8081`
- MongoDB Port: `27017`

## Development

### Project Structure
```
ProductManagement/
├── src/
│   ├── ProductManagement.Api/          # API Layer (Controllers, Middleware)
│   ├── ProductManagement.Application/  # Application Layer (Commands, Queries, DTOs)
│   ├── ProductManagement.Domain/       # Domain Layer (Entities, Events, Interfaces)
│   └── ProductManagement.Infrastructure/ # Infrastructure Layer (Data Access, Repositories)
├── docker-compose.yml                  # Docker configuration
├── Dockerfile                          # API Dockerfile
└── README.md                          # This file
```

### Adding New Features
1. Add domain entities/events to the Domain layer
2. Create application commands/queries in the Application layer
3. Implement repository interfaces in Domain and implementations in Infrastructure
4. Add controllers in the API layer
5. Update documentation as needed

### Testing Strategy
The project is designed with testability in mind:
- Domain logic is isolated and easily testable
- Dependencies are injected via DI container
- Repository interfaces allow for easy mocking
- CQRS pattern separates command and query logic

## Testing

While this project doesn't include explicit tests in the provided structure, the architecture supports comprehensive testing:

### Unit Tests
- Domain entities and value objects
- Application services
- Business logic methods
- Validation rules

### Integration Tests
- Repository implementations
- Database connectivity
- API endpoints
- CQRS handlers

## MongoDB Schema

The application uses a document-based schema for products:

```csharp
public class ProductDocument
{
    [BsonId]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; }
    public string Sku { get; set; }
    public int StockQuantity { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
```

## Security Considerations

- Connection strings should be stored in secure configuration
- API endpoints should be protected with authentication/authorization as needed
- Input validation prevents injection attacks
- HTTPS should be enforced in production

## Performance Optimization

- MongoDB indexes can be added for frequently queried fields
- Connection pooling is handled by the MongoDB driver
- Caching strategies can be implemented at the application level
- Proper indexing of MongoDB collections for optimal query performance

## Scaling Considerations

- MongoDB supports horizontal scaling through sharding
- API can be scaled independently using load balancers
- Read replicas can be used for read-heavy operations
- Connection pooling optimizes database resource usage

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

For support, please open an issue in the repository or contact the development team.