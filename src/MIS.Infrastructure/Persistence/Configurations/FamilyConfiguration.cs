using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Infrastructure.Persistence.Configurations;

public class FamilyConfiguration : IEntityTypeConfiguration<Family>
{
       public void Configure(EntityTypeBuilder<Family> builder)
       {
              // 1. Table Name and Primary Key
              builder.ToTable("Families");
              builder.HasKey(f => f.Id);

              // ==========================================
              // 2. CORE HOUSE RELATIONAL LINK
              // ==========================================
              // Assumes multiple families can reside within one physical building frame structure.
              // We use Restrict so deleting a structure accidentally won't cascade wipe your data collection logs.
              builder.HasOne(f => f.ResidenceHouse)
                     .WithMany()
                     .HasForeignKey(f => f.ResidenceHouseId)
                     .OnDelete(DeleteBehavior.Restrict);

              // ==========================================
              // 3. ONE-TO-MANY RELATIONSHIP (Members)
              // ==========================================
              // One family unit tracks multiple individual citizens.
              // Cascade delete ensures if a family log is completely deleted, its people records clear too.
              builder.HasMany(f => f.Members)
                     .WithOne(m => m.Family)
                     .HasForeignKey(m => m.FamilyId)
                     .OnDelete(DeleteBehavior.Cascade);

              builder.HasMany(f => f.Migrations)
                     .WithOne(m => m.Family)
                     .HasForeignKey(m => m.FamilyId)
                     .OnDelete(DeleteBehavior.Cascade);


              // ==========================================
              // 4. STRICT 1-TO-1 SUB-MODULE SURVEY SHARDS
              // ==========================================
              // For 1-to-1 relations, HasForeignKey<T> must specify the exact child table holding the FK.
              // Cascade delete ensures purging a family hub automatically purges its sub-form metrics.

              builder.HasOne(f => f.Agriculture)
                     .WithOne(a => a.Family)
                     .HasForeignKey<Agriculture>(a => a.FamilyId)
                     .OnDelete(DeleteBehavior.Cascade);

              builder.HasOne(f => f.Decision)
                     .WithOne(d => d.Family)
                     .HasForeignKey<Decision>(d => d.FamilyId)
                     .OnDelete(DeleteBehavior.Cascade);

              builder.HasOne(f => f.Disaster)
                     .WithOne(d => d.Family)
                     .HasForeignKey<Disaster>(d => d.FamilyId)
                     .OnDelete(DeleteBehavior.Cascade);

              builder.HasOne(f => f.Economic)
                     .WithOne(e => e.Family)
                     .HasForeignKey<Economy>(e => e.FamilyId)
                     .OnDelete(DeleteBehavior.Cascade);

              builder.HasOne(f => f.Facilities)
                     .WithOne(fac => fac.Family)
                     .HasForeignKey<Facility>(fac => fac.FamilyId)
                     .OnDelete(DeleteBehavior.Cascade);

              builder.HasOne(f => f.Health)
                     .WithOne(h => h.Family)
                     .HasForeignKey<Health>(h => h.FamilyId)
                     .OnDelete(DeleteBehavior.Cascade);

              builder.HasOne(f => f.Livestock)
                     .WithOne(l => l.Family)
                     .HasForeignKey<Livestock>(l => l.FamilyId)
                     .OnDelete(DeleteBehavior.Cascade);

              builder.HasOne(f => f.Social)
                     .WithOne(s => s.Family)
                     .HasForeignKey<Social>(s => s.FamilyId)
                     .OnDelete(DeleteBehavior.Cascade);
       }
}