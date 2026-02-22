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

    public DbSet<Area> Areas => Set<Area>();
    public DbSet<Tole> Toles => Set<Tole>();
    public DbSet<Ward> Wards => Set<Ward>();
    public DbSet<Municipality> Municipalities => Set<Municipality>();
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
