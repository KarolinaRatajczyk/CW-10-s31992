# CW-10-s31992

A C# and ASP.NET Core Web API for managing trips, clients and trip registrations. The application uses Entity Framework Core to access a SQL Server database.

## Features

- Retrieve a paginated list of trips.
- Include countries and clients associated with each trip.
- Register a client for a trip.
- Prevent registration for trips that have already started.
- Check whether a client with the same PESEL number is already registered for the trip.
- Delete a client only if they are not registered for any trips.

## API Endpoints

```http
GET /api/trips?page=1&pageSize=10
POST /api/trips/{idTrip}/clients
DELETE /api/clients/{idClient}
```

## Tech Stack

- C#
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server

## Getting Started

Requirements:

- .NET 8 SDK
- SQL Server
- An existing database matching the project's entity models

Configure the `Default-db` connection string in `CW10/appsettings.json`. The default connection points to an `apbd` database on a local SQL Server instance.

From the repository root:

```bash
cd CW10
dotnet restore
dotnet run
```

## Project Structure

```text
CW10/
├── Controllers/        # ClientsController and TripsController
├── DTOs/               # Trip and client data transfer objects
├── Models/             # Entity models and ApbdContext
├── Services/           # Trip and client management logic
├── Program.cs          # Application configuration
└── appsettings.json    # Database connection settings
```

## Notes

The repository does not include migrations or a database creation script. Prepare a database matching the entity models before running the application. Automated tests are not included.
