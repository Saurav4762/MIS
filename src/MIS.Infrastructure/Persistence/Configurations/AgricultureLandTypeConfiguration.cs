using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Infrastructure.Persistence.Configurations;

public class AgricultureLandTypeConfiguration : IEntityTypeConfiguration<AgricultureLandType>
{
    public void Configure(EntityTypeBuilder<AgricultureLandType> builder)
    {
        builder.ToTable("AgricultureLandTypes");

        builder.HasKey(lt => lt.Id);
        builder.Property(lt => lt.Id).ValueGeneratedOnAdd();

        builder.Property(lt => lt.Area).HasPrecision(18,4).IsRequired(false);

        builder.HasOne(lt => lt.Agriculture)
            .WithMany(a => a.LandTypes)
            .HasForeignKey(lt => lt.AgricultureId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_AgricultureLandType_Agriculture");

        builder.HasOne(lt => lt.LandType)
            .WithMany()
            .HasForeignKey(lt => lt.LandTypeId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_AgricultureLandType_LandType");

        builder.HasIndex(lt => lt.AgricultureId);
        builder.HasIndex(lt => lt.LandTypeId);
    }
}