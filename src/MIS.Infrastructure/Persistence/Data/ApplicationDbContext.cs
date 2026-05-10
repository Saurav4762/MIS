using Microsoft.EntityFrameworkCore;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Entities.HouseHold;
using MIS.Domain.Entities.Identity;
using OfficeOpenXml.Packaging.Ionic.Zip;

namespace MIS.Infrastructure.Persistence.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    // User Management
    public DbSet<User> Users => Set<User>();

    // Geography
    public DbSet<Province> Provinces => Set<Province>();
    public DbSet<District> Districts => Set<District>();
    public DbSet<Municipality> Municipalities => Set<Municipality>();
    public DbSet<Ward> Wards => Set<Ward>();
    public DbSet<Tole> Toles => Set<Tole>();

    // Household
    public DbSet<Person> Persons => Set<Person>();


    // Lookup tables
    public DbSet<OptionList> OptionLists => Set<OptionList>();
    public DbSet<OptionItem> OptionItems => Set<OptionItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
