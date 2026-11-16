# StudySpot

A personal catalog of places where you study well (libraries, cafes, university halls, a desk at home) with a log of the study sessions you spend there.

This is a learning project built with ASP.NET Core MVC. It is a local, single-user application without accounts.

## Features

- **Study places:** list, details, add, edit and delete (full CRUD). Each place has a name, a type, a location, optional notes, and whether it has Wi-Fi and power outlets.
- **Search and filter:** search places by name or location and filter them by type.
- **Study sessions:** record a session (place, date, duration in minutes, optional notes), see the full history (newest first) or only the sessions for one place, and delete a session.
- **Totals:** each place shows its number of sessions and total study time. The home page shows the five most recent sessions.
- **Validation:** on the server and in the browser. A session date cannot be in the future and its duration must be between 5 and 600 minutes.
- Deleting a place also deletes its sessions, after a confirmation page that shows how many there are.

## Technologies

- .NET 10, ASP.NET Core MVC with Razor views (layout, partial views, sections)
- Entity Framework Core 10 with SQL Server (LocalDB for development)
- Bootstrap 5, jQuery Validation (included in `StudySpot/wwwroot/lib`)

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- SQL Server LocalDB with the default instance `MSSQLLocalDB` (installed with Visual Studio or SQL Server Express), or another SQL Server 2019 or newer instance (see [Using another SQL Server](#using-another-sql-server))

## Setup and run

All commands are run from the repository root (the folder that contains `StudySpot.sln`).

1. Restore the local EF Core tool (`dotnet-ef`, defined in `dotnet-tools.json`) and the NuGet packages:

   ```bash
   dotnet tool restore
   dotnet restore
   ```

2. Create the database and apply the migrations:

   ```bash
   dotnet ef database update --project StudySpot
   ```

   This builds the project and creates the database `StudySpotDb` on `(localdb)\MSSQLLocalDB` with Windows authentication. This is the default connection string in `StudySpot/appsettings.json`:

   ```
   Server=(localdb)\MSSQLLocalDB;Database=StudySpotDb;Trusted_Connection=True
   ```

3. Start the application with the HTTP profile:

   ```bash
   dotnet run --project StudySpot --launch-profile http
   ```

   Then open http://localhost:5189.

### HTTPS

The `https` profile listens on https://localhost:7277 and http://localhost:5189. Trust the ASP.NET Core development certificate once, then start the profile:

```bash
dotnet dev-certs https --trust
dotnet run --project StudySpot --launch-profile https
```

## Initial data

The migrations add five place types (Library, Cafe, University, Home, Other) and four sample study places. No study sessions are added. Running `dotnet ef database update` again does not duplicate this data.

## Using another SQL Server

No environment variables are required with LocalDB. To use another SQL Server instance or database name, set the `ConnectionStrings__DefaultConnection` environment variable instead of editing the code. Set it in the same terminal, before both the migration and the run commands. For example, in PowerShell:

```powershell
$env:ConnectionStrings__DefaultConnection = "Server=localhost;Database=StudySpotDb;Trusted_Connection=True;TrustServerCertificate=True"
dotnet ef database update --project StudySpot
dotnet run --project StudySpot --launch-profile http
```

In Bash, use `export ConnectionStrings__DefaultConnection="..."` instead of the first line.

## Limitations

- Single user and local only: there are no accounts, roles or sharing.
- LocalDB runs only on Windows. On Linux or macOS, use another SQL Server instance as shown above.
- The database uses SQL Server 2019 compatibility (level 150), so SQL Server 2019 or newer is required.
- Study sessions can be recorded and deleted, but not edited.
- "Today" for the session date check is the date of the machine that runs the application.
- Search case sensitivity follows the database collation (case-insensitive with the SQL Server default).
