# StudySpot

A personal catalog of places where you study well (libraries, cafes, university halls, home) with a log of the study sessions you spend there.

This is a learning project built with ASP.NET Core MVC. It is a local, single-user application.

> Work in progress: the project is being built stage by stage. This README is completed at the end.

## Technologies

- .NET 10 (ASP.NET Core MVC, Razor views)
- Bootstrap (included with the project template)
- Entity Framework Core 10 with SQL Server

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- SQL Server LocalDB (installed with Visual Studio or SQL Server Express), instance `MSSQLLocalDB`

## Database

The default connection string in `StudySpot/appsettings.json` uses LocalDB with Windows authentication and the database `StudySpotDb`:

```
Server=(localdb)\MSSQLLocalDB;Database=StudySpotDb;Trusted_Connection=True
```

From the repository root, restore the local EF Core tool and apply the migrations:

```bash
dotnet tool restore
dotnet ef database update --project StudySpot
```

To use another SQL Server instance, set the `ConnectionStrings__DefaultConnection` environment variable instead of editing the code. For example, in PowerShell:

```powershell
$env:ConnectionStrings__DefaultConnection = "Server=localhost;Database=StudySpotDb;Trusted_Connection=True;TrustServerCertificate=True"
```

Set it in the same terminal before running the migration and the application.

## Run

From the repository root:

```bash
dotnet run --project StudySpot --launch-profile http
```

Then open http://localhost:5189.

To use HTTPS instead, trust the development certificate once and use the `https` profile:

```bash
dotnet dev-certs https --trust
dotnet run --project StudySpot --launch-profile https
```

The HTTPS address is https://localhost:7277.
