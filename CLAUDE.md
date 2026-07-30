# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Run

```bash
# Build the entire solution
dotnet build mis.slnx

# Run the API (starts on http://localhost:5242, Swagger at /swagger)
dotnet run --project src/MIS.API

# Run all tests
dotnet test mis.slnx

# Run tests for a specific project
dotnet test tests/MIS.Appliaction.Tests
dotnet test tests/MIS.Domain.Tests

# Run a single test class
dotnet test --filter "FullyQualifiedName~ProvinceServiceTest"
```

## Architecture

**Clean Architecture** (4 layers, dependencies point inward):

```
MIS.Domain          — No dependencies. Entities + exception hierarchy.
MIS.Application     — Depends on Domain. Services, DTOs, validators, repository interfaces.
MIS.Infrastructure  — Depends on Application + Domain. EF Core DbContext, repo implementations, JWT, Excel.
MIS.API             — Depends on Application + Infrastructure. Controllers, middleware, response envelope.
```

### Layer Details

**Domain** (`src/MIS.Domain/`)
- `Entities/` — organized by feature: `Geography/`, `Identity/`, `DataCollection/HouseInfo/`, `DataCollection/HouseholdInfo/`, `Options/`, `Submissions/`
- `Exceptions/` — `BaseException` (abstract) with subclasses: `BadRequestException`, `NotFoundException`, `ConflictException`, `UnauthorizedException`, `ForbiddenException`, `InternalServerException`, `DatabaseException`, `DataValidationException`. Each carries an `ErrorCode` string and optional `Details` dict.
- `Common/Premitives/BaseEntity.cs` — provides `Guid Id` to all entities.

**Application** (`src/MIS.Application/`)
- `Features/{FeatureName}/` — each feature has: service interface (`I{Name}Service`), service implementation (`{Name}Service`), DTOs (`{Name}Dto.cs`, `Create{Name}DTO.cs`, `Update{Name}DTO.cs`), FluentValidation validators.
- `Common/Extensions/` — `ValidationExtensions` (`EnsureValidOrThrowAsync`) converts validation failures to `DataValidationException`. `NepaliValidatorExtensions` for Devanagari text validation.
- `Common/Interfaces/IUnitOfWork.cs` — exposes all repository interfaces (used inconsistently; most services inject repos directly).
- DI registration in `DipendencyInjection.cs` (note: typo in filename — `Dipendency` not `Dependency`). Registers all service interfaces + FluentValidation validators via `AddValidatorsFromAssemblyContaining`.

**Infrastructure** (`src/MIS.Infrastructure/`)
- `Persistence/Data/ApplicationDbContext.cs` — EF Core DbContext. Uses `ApplyConfigurationsFromAssembly`. Connected to PostgreSQL via Npgsql 10.0.1 with NetTopologySuite.
- `Persistence/Configurations/` — `IEntityTypeConfiguration<T>` classes per entity.
- `Persistence/Repositories/` — organized by feature. `BaseRepo<T>` base class. Repos call `_context.SaveChangesAsync()` directly per operation.
- `Persistence/UnitOfWork.cs` — `IUnitOfWork` implementation with all repository properties injected via constructor.
- `Migrations/` — 15 EF Core migrations tracking schema evolution.
- `Identity/` — `JwtTokenService` (token generation), `PasswordHashService` (BCrypt wrapper).
- `ExcelParser/` — `MunicipalityExcelParser` (seeding), `ExcelGenerator` (export with `[ExcelColumn]` attribute).

**API** (`src/MIS.API/`)
- `Features/{FeatureName}/{Name}Controller.cs` — controllers call service methods, return `ApiResponse<T>`.
- `Common/Middlewares/GlobalExceptionHandler.cs` — catches all `BaseException` subtypes and maps to typed `ApiResponse<T>`.
- `Common/Responses/ApiResponse.cs` — unified response envelope with `Success`, `Message`, `Data`, `Error`, `StatusCode`, `Timestamp`, and pagination fields.
- `Program.cs` — JWT Bearer auth (HMAC-SHA256), CORS (AllowAnyOrigin/Method/Header), Swagger, OpenAPI.

### DI Registration Flow

```
Program.cs → AddInfrastructure(config) → registers repos, identity services, DbContext
           → AddApplication()          → registers service classes + FluentValidation validators
```

## Key Patterns

### Controller → Service → Repository → DbContext
All features follow this same pattern. Controllers never touch DbContext directly.

### Response Envelope
Every endpoint returns `ApiResponse<T>` via static factory methods:
- `ApiResponse<T>.SuccessResponse(data, message?)` — 200 OK
- `ApiResponse<T>.FailResponse(message, error?)` — error responses
- `ApiResponse<T>.Paginated(data, page, pageSize, totalCount?)` — paginated results

### Validation
DTOs have FluentValidation validators. Services call `await validator.EnsureValidOrThrowAsync(dto)` which throws `DataValidationException` on failure. The `GlobalExceptionHandler` middleware catches it and returns `400 BadRequest` with error details.

### Exception-Driven Error Flow
Services throw typed exceptions (`NotFoundException`, `ConflictException`, etc.). The `GlobalExceptionHandler` middleware catches them and maps to the right HTTP status code automatically.

### Geography Hierarchy (Nepal administrative structure)
```
Province → District → Area → Municipality → Ward → Tole
```
Each entity has foreign keys to its parent. CRUD for all 6 entities exists.

### Dynamic Lookups (OptionList / OptionItem)
Instead of hardcoded enums, dropdown values use `OptionList` → `OptionItem` with `Dictionary<string, object>? Extra` for extensible metadata (serialized via Npgsql's `EnableDynamicJson()`).

### Data Collection Structure
```
House (submission + location) → Family (resident group)
  ├── Member (individual person data)
  ├── Economy (income/expenditure)
  ├── Facility (utilities/appliances)
  ├── Health (insurance, chronic illness)
  ├── Agriculture (land, crops, equipment)
  ├── Livestock (animals, veterinary services)
  ├── Decision (household decision-making)
  ├── Disaster (threats, history, preparedness)
  ├── Social (ethnicity, religion, language)
  └── Migration (origin, reason)
```

### Auth
`POST /api/Auth` with `{Email, Password}` → returns `{AccessToken, ExpiresAtUtc}`. Only `SubmissionsController` is `[Authorize]d`; all other controllers are currently unauthenticated.

## Project Quirks

- `src/MIS.Application/DipendencyInjection.cs` — filename is a typo (intentional, not to be renamed).
- `tests/MIS.Appliaction.Tests/` — folder name has the same typo.
- Several controllers are missing `[Authorize]` (only SubmissionsController has it).
- UnitOfWork is registered but not consistently used — most services call repo methods and `SaveChangesAsync` directly on injected repos.
- Entity `Economy` is in the `Economic.cs` file in the Domain layer but references `Economy` elsewhere.

## graphify

This project has a knowledge graph at graphify-out/ with god nodes, community structure, and cross-file relationships.

Rules:
- For codebase questions, first run `graphify query "<question>"` when graphify-out/graph.json exists. Use `graphify path "<A>" "<B>"` for relationships and `graphify explain "<concept>"` for focused concepts. These return a scoped subgraph, usually much smaller than GRAPH_REPORT.md or raw grep output.
- If graphify-out/wiki/index.md exists, use it for broad navigation instead of raw source browsing.
- Read graphify-out/GRAPH_REPORT.md only for broad architecture review or when query/path/explain do not surface enough context.
- After modifying code, run `graphify update .` to keep the graph current (AST-only, no API cost).
