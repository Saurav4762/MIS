using Microsoft.EntityFrameworkCore;
using MIS.API.Models;
using NetTopologySuite.Geometries;

namespace MIS.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // User & role management
    public DbSet<AppUser> AppUsers => Set<AppUser>();
    public DbSet<AppRole> AppRoles => Set<AppRole>();
    public DbSet<AppUserRole> AppUserRoles => Set<AppUserRole>();

    // Lookup tables
    public DbSet<OptionList> OptionLists => Set<OptionList>();
    public DbSet<OptionItem> OptionItems => Set<OptionItem>();
    public DbSet<Submission> Submissions => Set<Submission>();

    public DbSet<Municipality> Municipalities => Set<Municipality>();
    public DbSet<Ward> Wards => Set<Ward>();
    public DbSet<Tole> Toles => Set<Tole>();

    public DbSet<House> Houses => Set<House>();
    public DbSet<Family> Families => Set<Family>();
    public DbSet<Person> Persons => Set<Person>();

    public DbSet<Ethnicity> Ethnicities => Set<Ethnicity>();
    public DbSet<Religion> Religions => Set<Religion>();
    public DbSet<HouseOwner> HouseholdHeads => Set<HouseOwner>();
    public DbSet<Education> Educations => Set<Education>();
    public DbSet<Institute> Institutes => Set<Institute>();

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
