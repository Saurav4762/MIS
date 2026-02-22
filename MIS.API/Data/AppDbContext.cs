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
            entity.HasIndex(oi => new { oi.OptionListId, oi.Code }).IsUnique();
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

        modelBuilder.Entity<Household>(entity =>
        {
            entity.HasOne(h => h.Submission)
            .WithMany(s => s.Households)
            .HasForeignKey(h => h.SubmissionId)
            .OnDelete(DeleteBehavior.Cascade);

            entity.Property(h => h.Location).HasColumnType("geography (point)");
        });

        modelBuilder.Entity<Tole>(entity =>
        {
            entity.HasOne(t => t.Area)
                .WithMany(a => a.Toles)
                .HasForeignKey(t => t.AreaId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasOne(a => a.Ward)
            .WithMany(w => w.Areas)
            .HasForeignKey(a => a.WardId)
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Ward>(entity =>
        {
            entity.HasOne(w => w.Municipality)
            .WithMany(m => m.Wards)
            .HasForeignKey(w => w.MunicipalityId)
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<HouseInfo>(entity =>
        {
            entity.Property(h => h.Coords).HasColumnType("geography (point)");
            entity.HasOne(h => h.Tole)
                .WithMany(t => t.HouseInfos)
                .HasForeignKey(h => h.ToleId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        //optionList and optionItem relationships for sanitation and hygiene and family decision
        modelBuilder.Entity<SanitationandHygine>(entity =>
        {
            entity.HasKey(sh => sh.Id);

            entity.HasOne(s => s.SourceOfWater)
            .WithMany()
            .HasForeignKey(s => s.SourceOfWaterId)
            .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(s => s.TypeOfToiletOption)
            .WithMany()
            .HasForeignKey(s => s.TypeOfToiletId)
            .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(s => s.HandwashFacility)
            .WithMany() 
            .HasForeignKey(s => s.HandwashFacilityId)
            .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(sh => sh.Household)
            .WithOne(h => h.SanitationandHygine)
            .HasForeignKey<SanitationandHygine>(sh => sh.HouseholdId)
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OptionList>().HasData(
            new OptionList
            {
                Id = Guid.Parse("11111111-2222-3333-4444-555555555555"),
                Code = "source_of_water",
                LabelEn = "Source of Water",
                LabelNe = "पानीको स्रोत",
                Description = "Options for source of water"
            },
            new OptionList
            {
                Id = Guid.Parse("22222222-3333-4444-5555-666666666666"),
                Code = "type_of_toilet",
                LabelEn = "Type of Toilet",
                LabelNe = "टॉयलेटको प्रकार",
                Description = "Options for type of toilet"
            },
            new OptionList
            {
                Id = Guid.Parse("33333333-4444-5555-6666-777777777777"),
                Code = "handwash_facility",
                LabelEn = "Handwash Facility",
                LabelNe = "हात धुने सुविधा",
                Description = "Options for handwash facility"
            }
        );

        modelBuilder.Entity<OptionItem>().HasData(
            // Source of Water options
            new OptionItem
            {
                Id = Guid.Parse("b1e2c3d4-f5a6-4b7c-8d9e-0f1a2b3c4d5e"),
                OptionListId = Guid.Parse("11111111-2222-3333-4444-555555555555"),
                Code = "tap_water",
                LabelEn = "Tap Water",
                LabelNe = "ट्याप पानी",
                IsActive = true
            },
            new OptionItem
            {
                Id = Guid.Parse("c2d3e4f5-a6b7-4c8d-9e0f-1a2b3c4d5e6f"),
                OptionListId = Guid.Parse("11111111-2222-3333-4444-555555555555"),
                Code = "well_water",
                LabelEn = "Well Water",
                LabelNe = "कुवा पानी",
                IsActive = true
            },
            new OptionItem
            {
                Id = Guid.Parse("d3e4f5a6-b7c8-4d9e-0f1a-2b3c4d5e6f7a"),
                OptionListId = Guid.Parse("11111111-2222-3333-4444-555555555555"),
                Code = "river_water",
                LabelEn = "River Water",
                LabelNe = "नदी पानी",
                IsActive = true
            },
            new OptionItem
            {
                Id = Guid.Parse("e4f5a6b7-c8d9-4e0f-1a2b-3c4d5e6f7a8b"),
                OptionListId = Guid.Parse("11111111-2222-3333-4444-555555555555"),
                Code = "rain_water",
                LabelEn = "Rain Water",
                LabelNe = "वर्षा पानी",
                IsActive = true
            },
            new OptionItem
            {
                Id = Guid.Parse("f5a6b7c8-d9e0-4f1a-2b3c-4d5e6f7a8b9c"),
                OptionListId = Guid.Parse("11111111-2222-3333-4444-555555555555"),
                Code = "bottled_water",
                LabelEn = "Bottled Water",
                LabelNe = "बोतल पानी",
                IsActive = true
            },
            new OptionItem
            {
                Id = Guid.Parse("a6b7c8d9-e0f1-4a2b-3c4d-5e6f7a8b9c0d"),
                OptionListId = Guid.Parse("11111111-2222-3333-4444-555555555555"),
                Code = "other",
                LabelEn = "Other",
                LabelNe = "अन्य",
                IsActive = true
            },
            // Type of Toilet options
            new OptionItem
            {
                Id = Guid.Parse("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"),
                OptionListId = Guid.Parse("22222222-3333-4444-5555-666666666666"),
                Code = "flush_toilet",
                LabelEn = "Flush Toilet",
                LabelNe = "फ्लश टॉयलेट",
                IsActive = true
            },
            new OptionItem
            {
                Id = Guid.Parse("b7c8d9e0-f1a2-4b3c-4d5e-6f7a8b9c0d1e"),
                OptionListId = Guid.Parse("22222222-3333-4444-5555-666666666666"),
                Code = "pit_latrine",
                LabelEn = "Pit Latrine",
                LabelNe = "पिट लाट्रिन",
                IsActive = true
            },
            new OptionItem
            {
                Id = Guid.Parse("c8d9e0f1-a2b3-4c4d-5e6f-7a8b9c0d1e2f"),
                OptionListId = Guid.Parse("22222222-3333-4444-5555-666666666666"),
                Code = "composting_toilet",
                LabelEn = "Composting Toilet",
                LabelNe = "कम्पोस्टिंग टॉयलेट",
                IsActive = true
            },
            new OptionItem
            {
                Id = Guid.Parse("d9e0f1a2-b3c4-4d5e-6f7a-8b9c0d1e2f3a"),
                OptionListId = Guid.Parse("22222222-3333-4444-5555-666666666666"),
                Code = "other",
                LabelEn = "Other",
                LabelNe = "अन्य",
                IsActive = true
            },
            // Handwash Facility options
            new OptionItem
            {
                Id = Guid.Parse("e0f1a2b3-c4d5-4e6f-7a8b-9c0d1e2f3a4b"),
                OptionListId = Guid.Parse("33333333-4444-5555-6666-777777777777"),
                Code = "handwash_station",
                LabelEn = "Handwash Station",
                LabelNe = "हात धुने स्टेशन",
                IsActive = true
            },
            new OptionItem
            {
                Id = Guid.Parse("f1a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c"),
                OptionListId = Guid.Parse("33333333-4444-5555-6666-777777777777"),
                Code = "handwash_basin",
                LabelEn = "Handwash Basin",
                LabelNe = "हात धुने बासिन",
                IsActive = true
            },
            new OptionItem
            {
                Id = Guid.Parse("a2b3c4d5-e6f7-4a8b-9c0d-1e2f3a4b5c6d"),
                OptionListId = Guid.Parse("33333333-4444-5555-6666-777777777777"),
                Code = "handwash_bucket",
                LabelEn = "Handwash Bucket",
                LabelNe = "हात धुने बाल्टी",
                IsActive = true
            },
            new OptionItem
            {
                Id = Guid.Parse("b3c4d5e6-f7a8-4b9c-0d1e-2f3a4b5c6d7e"),
                OptionListId = Guid.Parse("33333333-4444-5555-6666-777777777777"),
                Code = "other",
                LabelEn = "Other",
                LabelNe = "अन्य",
                IsActive = true
            }
        );

        modelBuilder.Entity<FamilyDecision>(entity =>
        {
            entity.HasOne(fd => fd.Household)
            .WithOne(h => h.FamilyDecision)
            .HasForeignKey<FamilyDecision>(fd => fd.HouseholdId)
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Household>(entity =>
        {
            entity.HasOne(h => h.HouseInfo)
                .WithMany(hh => hh.Households)
                .HasForeignKey(h => h.HouseInfoId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
