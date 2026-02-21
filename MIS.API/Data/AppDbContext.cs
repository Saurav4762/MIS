using Microsoft.EntityFrameworkCore;
using MIS.API.Models;

namespace MIS.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // // Lookup tables
    // public DbSet<OptionList> OptionLists => Set<OptionList>();
    // public DbSet<OptionItem> OptionItems => Set<OptionItem>();

    // User & role management
    public DbSet<AppUser> AppUsers => Set<AppUser>();
    public DbSet<AppRole> AppRoles => Set<AppRole>();
    public DbSet<AppUserRole> AppUserRoles => Set<AppUserRole>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<AppRole>(entity =>
        {
            entity.HasIndex(r => r.RoleCode).IsUnique();
            entity.Property(r => r.RoleCode).IsRequired();
        });

        modelBuilder.Entity<AppUserRole>(entity =>
        {
            entity.HasKey(aur => new { aur.AppUserId, aur.AppRoleId });
        });
    }
}
