# Architecture Documentation: Report Service Refactoring

## Overview
This document explains the proper architectural flow of the MIS backend and the report service organization.

## Layered Architecture

### Layer 1: API Layer (`MIS.API`)
**Responsibility:** HTTP Request Handling
- Controllers receive HTTP requests
- Validate incoming requests
- Call Application Layer services
- Return HTTP responses

**Example:** `ReportsController.cs` - Routes requests to `IReportService`

---

### Layer 2: Application Layer (`MIS.Application`)
**Responsibility:** Business Logic & Orchestration
- Contains use cases and business logic
- Orchestrates between domain and infrastructure
- Service interfaces and implementations
- DTOs (Data Transfer Objects)
- Business rule validation
- **Report generation orchestration** ✅

**Key Classes:**
- `IReportService` (Interface) - Defines contract for report operations
- `ReportService` (Implementation) - Orchestrates specialized report services

**Why ReportService belongs here:**
- Reports are a business use case, not just data access
- Orchestrates multiple data queries into business reports
- Implements high-level report generation logic

---

### Layer 3: Infrastructure Layer (`MIS.Infrastructure`)
**Responsibility:** Technical Implementation Details
- Database access (DbContext, Repositories)
- External integrations
- Data persistence patterns
- Specialized query services

**Key Components:**
- `ApplicationDbContext` - EF Core Database context
- `Repositories` - Data access patterns (CRUD operations)
- **Specialized Report Query Services** ✅
  - `PopulationReportService` - Population data queries
  - `HouseholdReportService` - Household data queries
  - `FamilyReportService` - Family data queries
  - `GeographicReportService` - Geographic data queries
  - `DataQualityReportService` - Data quality queries

**Why these belong here:**
- They handle direct database queries
- They are implementation-specific (can change based on DB changes)
- They are pure data access utilities

---

### Layer 4: Domain Layer (`MIS.Domain`)
**Responsibility:** Business Entities & Rules
- Domain entities (no ORM dependencies)
- Business rules and validations
- Domain exceptions
- Core business logic that doesn't change with implementation

---

## Data Flow

```
API Request
    ↓
ReportsController (API Layer)
    ↓
IReportService.GetPopulationByGenderAsync() (Application Layer - Orchestrator)
    ↓
PopulationReportService.GetPopulationByGenderAsync() (Infrastructure Layer - Data Query)
    ↓
ApplicationDbContext (Infrastructure Layer - Database Access)
    ↓
Database
    ↓
ChartDataResponse (DTO returned back up the stack)
```

## Dependency Flow (Correct - Depends Downward)

```
API Layer
    ↓ depends on
Application Layer
    ↓ depends on
Infrastructure Layer
    ↓ depends on
Domain Layer
```

**Rule:** Higher layers should NEVER directly depend on lower layer implementations.
- ✅ API can call Application services
- ✅ Application can use Infrastructure query helpers
- ✅ Infrastructure can access Domain entities
- ❌ Domain should NOT depend on Infrastructure
- ❌ Infrastructure should NOT depend on API

## Why This Refactoring Was Necessary

**Before (Incorrect):**
```
ReportService in Infrastructure Layer
↓ Problem: Business logic in infrastructure layer
↓ Violates Dependency Inversion Principle
↓ Makes it hard to test business logic
↓ Couples business logic to infrastructure concerns
```

**After (Correct):**
```
ReportService in Application Layer (Orchestrator)
    ↓
PopulationReportService in Infrastructure Layer (Data Query Helper)
    ↓ Separation of concerns
    ↓ Business logic separate from data access
    ↓ Easy to test and maintain
    ↓ Easy to change database without affecting business logic
```

## Dependency Injection

### Application Layer (`MIS.Application/DependencyInjection.cs`)
```csharp
services.AddScoped<IReportService, ReportService>();
```
✅ Application layer registers its own services

### Infrastructure Layer (`MIS.Infrastructure/DependencyInjection.cs`)
```csharp
// No explicit registration needed for ReportService
// Specialized services are created internally by ReportService
// They are infrastructure utilities, not injectable services
```

## Benefits of This Architecture

1. **Separation of Concerns**
   - Business logic (Application) is separate from data access (Infrastructure)

2. **Testability**
   - Easy to mock Infrastructure services when testing Application layer
   - Business logic can be tested independently

3. **Maintainability**
   - Clear responsibility for each layer
   - Easy to find where each concern is handled

4. **Flexibility**
   - Easy to swap Infrastructure implementations (e.g., different databases)
   - Business logic remains unchanged

5. **Scalability**
   - Each layer can evolve independently
   - Easy to add new features without affecting other layers

## Adding New Reports

When adding a new report:

1. **Add method to** `IReportService` (Application layer - defines contract)
2. **Implement in** `ReportService` (Application layer - orchestrator)
3. **Create specialized service** in Infrastructure (data query helper)
   - Example: `NewReportService` in `Infrastructure/Persistence/Reports/NewReport/`
4. **Wire up in** `ReportService` constructor
5. **Update** `ReportsController` with new endpoint

## Summary

The proper architectural flow follows Clean Architecture principles:
- **API Layer** handles HTTP concerns
- **Application Layer** handles business logic and orchestration ← ReportService lives here
- **Infrastructure Layer** handles data access and technical details ← Specialized query services live here
- **Domain Layer** contains pure business entities and rules

This separation ensures a maintainable, testable, and scalable codebase.

