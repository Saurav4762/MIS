using Microsoft.EntityFrameworkCore;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;
using MIS.Domain.Entities.DataCollection.HouseInfo;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Entities.Identity;
using MIS.Domain.Entities.Submissions;
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



    // ====================== Household Info ======================
    public DbSet<House> Houses => Set<House>();
    public DbSet<Family> Families => Set<Family>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Facility> Facilities => Set<Facility>();
    public DbSet<Social> Socials => Set<Social>();
    public DbSet<Migration> Residences => Set<Migration>();

    // ====================== Other Data Collection ======================
    public DbSet<Agriculture> Agricultures => Set<Agriculture>();
    public DbSet<Decision> Decisions => Set<Decision>();
    public DbSet<Disaster> Disasters => Set<Disaster>();
    public DbSet<Economy> Economics => Set<Economy>();
    public DbSet<Health> Healths => Set<Health>();
    public DbSet<Livestock> Livestocks => Set<Livestock>();


    // Lookup tables
    public DbSet<OptionList> OptionLists => Set<OptionList>();
    public DbSet<OptionItem> OptionItems => Set<OptionItem>();

    // Submissions
    public DbSet<Submission> Submissions => Set<Submission>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
