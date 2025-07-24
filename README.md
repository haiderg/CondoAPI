# CondoAPI

A RESTful API for managing condominium data built with ASP.NET Core 8.

## Features

- **ASP.NET Core 8**: Modern web API framework
- **SQL Server**: Robust relational database
- **Dapper**: High-performance micro-ORM
- **Global Exception Handling**: Centralized error handling
- **Serilog**: Structured logging
- **Unit Testing**: XUnit with Moq and FluentAssertions
- **Swagger**: API documentation

## Project Structure

The solution follows a clean architecture approach:

- **CondoAPI.API**: API controllers, middleware, and configuration
- **CondoAPI.Core**: Domain models, interfaces, and business logic
- **CondoAPI.Infrastructure**: Data access and external services
- **CondoAPI.Tests**: Unit tests

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server

### Database Setup

1. Open SQL Server Management Studio or Azure Data Studio
2. Run the script in `Database/CreateDatabase.sql` to create the database and tables

### Configuration

Update the connection string in `appsettings.json` if needed:

```json
"DatabaseSettings": {
  "ConnectionString": "Server=localhost;Database=CondoDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### Running the API

```bash
cd CondoAPI.API
dotnet run
```

The API will be available at:
- https://localhost:5001
- http://localhost:5000

Swagger documentation will be available at:
- https://localhost:5001/swagger
- http://localhost:5000/swagger

### Running Tests

```bash
cd CondoAPI.Tests
dotnet test
```

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET    | /api/residents | Get all residents |
| GET    | /api/residents/active | Get active residents |
| GET    | /api/residents/{id} | Get resident by ID |
| POST   | /api/residents | Create a new resident |
| PUT    | /api/residents/{id} | Update a resident |
| DELETE | /api/residents/{id} | Delete a resident |

## License

This project is licensed under the MIT License.