# StudySpot

A personal catalog of places where you study well (libraries, cafes, university halls, home) with a log of the study sessions you spend there.

This is a learning project built with ASP.NET Core MVC. It is a local, single-user application.

> Work in progress: the project is being built stage by stage. This README is completed at the end.

## Technologies

- .NET 10 (ASP.NET Core MVC, Razor views)
- Bootstrap (included with the project template)
- Entity Framework Core with SQL Server (added in the next stages)

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

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
