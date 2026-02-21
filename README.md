# Management Information System (MIS) API

A comprehensive ASP.NET Core Web API for managing user information, submissions, and configuration data with PostgreSQL database support.

## Application Overview

The MIS API is built on **.NET 10.0** with the following core features:

- **User Management**: Role-based user authentication and authorization
- **Data Configuration**: Dropdown lists and option items for application configuration
- **Submission Tracking**: Track and manage user submissions
- **Spatial Data Support**: NetTopologySuite integration for geographic data
- **API Documentation**: Built-in Swagger/OpenAPI documentation
- **Database**: PostgreSQL with Entity Framework Core ORM

## System Requirements

Before starting the application, ensure you have the following installed:

### Required Software

1. **.NET 10.0 SDK** - Download from [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)
   - Verify installation: `dotnet --version`

2. **PostgreSQL 12+** - Download from [https://www.postgresql.org/download/](https://www.postgresql.org/download/)
   - Verify installation: `psql --version`

3. **PostGIS Extension** - Required for spatial data support
   - Installation: `CREATE EXTENSION postgis;` (run in PostgreSQL)
   - Download/Info: [https://postgis.net/](https://postgis.net/)
   - Note: Most PostgreSQL installers include PostGIS; enable it during installation or add it later using SQL

4. **Git** - Download from [https://git-scm.com/](https://git-scm.com/)

5. **A Code Editor** (recommended):
   - Visual Studio Code
   - Visual Studio Community
   - JetBrains Rider

### Recommended Development Tools

- PostgreSQL Admin Tool (pgAdmin or DBeaver)
- Postman or Thunder Client for API testing

## Getting Started

### Step 1: Clone the Repository

```bash
git clone <repository-url>
cd Management_Information_System
```

### Step 2: Restore Dependencies

```bash
dotnet restore
```

### Step 3: Configure the Database Connection

1. **Create a PostgreSQL Database with PostGIS**:
   ```sql
   CREATE DATABASE MISDB;
   \c MISDB
   CREATE EXTENSION postgis;
   ```

2. **Update Connection String** (if needed):
   - Open `MIS.API/appsettings.json` or `MIS.API/appsettings.Development.json`
   - Modify the `DefaultConnection` if your PostgreSQL is not running on localhost or uses different credentials:
   
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Username=postgres;Password=password;Port=5432;Database=MISDB"
   }
   ```

### Step 4: Apply Database Migrations

```bash
cd MIS.API
dotnet ef database update
```

This will create all necessary tables and schemas in the PostgreSQL database.

### Step 5: Run the Application

#### Option A: Using the Watch Task (Recommended for Development)
```bash
dotnet watch run --project MIS.API
```
This will automatically rebuild and restart the application when you save changes.

#### Option B: Direct Run
```bash
dotnet run --project MIS.API
```

#### Option C: Using VS Code Task
- Press `Ctrl + Shift + D` to open the Run and Debug view
- Select the "watch" task and press F5

### Step 6: Access the API

Once the application is running, access it via:

- **API Base URL**: `https://localhost:7001` or `http://localhost:5000`
- **Swagger Documentation**: `https://localhost:7001/swagger` or `http://localhost:5000/swagger`

Use Swagger to test all available endpoints.

## Project Structure

```
MIS.API/
├── Controllers/       # API endpoints
├── Models/           # Data models (AppUser, AppRole, Submission, etc.)
├── Data/             # Entity Framework Core DbContext
├── Migrations/       # Database migration files
├── appsettings.json  # Configuration files
└── Program.cs        # Application startup configuration
```

## Database Schema

The application manages the following key entities:

- **AppUser**: System users
- **AppRole**: User roles and permissions
- **AppUserRole**: User-role relationships
- **OptionList**: Dropdown list configurations
- **OptionItem**: Items within dropdown lists
- **Submission**: User submissions and records

## Common Commands

### Build the Project
```bash
dotnet build
```

### Run in Production Mode
```bash
dotnet publish -c Release
```

### Create a New Migration
```bash
dotnet ef migrations add <MigrationName> --project MIS.API
```

### View Database Updates
```bash
dotnet ef database update --project MIS.API
```

### Run Tests (if available)
```bash
dotnet test
```

## Troubleshooting

### Database Connection Failed
- Ensure PostgreSQL is running
- Check connection string in appsettings.json
- Verify database exists with correct name
- Test connection: `psql -U postgres -d MISDB -h localhost`

### Migrations Failed
- Clear pending migrations: `dotnet ef migrations remove --project MIS.API`
- Ensure database exists before running migrations
- Check if a migration with the same name already exists

### Port Already in Use
- Change the port in `Properties/launchSettings.json`
- Or stop the application using that port

### NetTopologySuite Errors
- Ensure PostGIS is enabled in PostgreSQL (if using spatial features)
- Rebuild the project after adding spatial data models

## Support

For issues or questions:
1. Check the [.NET Documentation](https://learn.microsoft.com/en-us/dotnet/)
2. Review [Entity Framework Core Documentation](https://learn.microsoft.com/en-us/ef/core/)
3. Check PostgreSQL logs for database-related issues

## License

[Add your license information here]

---

**Last Updated**: February 21, 2026
