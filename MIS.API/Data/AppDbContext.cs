using Microsoft.EntityFrameworkCore;
using MIS.API.Models;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User & role management
        modelBuilder.Entity<AppRole>(entity =>
        {
            entity.HasIndex(r => r.RoleCode).IsUnique();
            entity.Property(r => r.RoleCode).IsRequired();
        });

        modelBuilder.Entity<AppUserRole>(entity =>
        {
            entity.HasKey(aur => new { aur.AppUserId, aur.AppRoleId });
        });

        // Lookup tables
        modelBuilder.Entity<OptionList>(entity =>
        {
            entity.HasIndex(ol => ol.Code).IsUnique();
            entity.Property(ol => ol.Code).IsRequired();
            
            entity.Property(ol => ol.LabelEn).IsRequired();
            entity.Property(ol => ol.LabelNe).IsRequired();
        });

        modelBuilder.Entity<OptionItem>(entity =>
        {
            entity.HasIndex(oi => oi.Code).IsUnique();
            entity.Property(oi => oi.Code).IsRequired();

            entity.Property(oi => oi.LabelEn).IsRequired();
            entity.Property(oi => oi.LabelNe).IsRequired();

            entity.HasOne(oi => oi.OptionList)
            .WithMany(ol => ol.OptionItems)
            .HasForeignKey(oi => oi.OptionListId)
            .OnDelete(DeleteBehavior.Cascade);

            entity.Property(oi => oi.Extra).HasColumnType("jsonb");

        });

        modelBuilder.Entity<Submission>(entity =>
        {
            entity.HasOne(s => s.CreatedBy)
            .WithMany(u => u.Submissions)
            .HasForeignKey(s => s.CreatedById)
            .OnDelete(DeleteBehavior.SetNull);

            entity.Property(s => s.StartGeopoint).HasColumnType("geography (point)");
            entity.Property(s => s.RawSubmissionJson).HasColumnType("jsonb");
        });

    }
}
