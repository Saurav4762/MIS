using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.API.Models;

namespace MIS.API.Data.Configurations;

public class HouseOwnerConfiguration : IEntityTypeConfiguration<HouseOwner>
{
    public void Configure(EntityTypeBuilder<HouseOwner> builder)
    {
        builder.ToTable("HouseOwners");
        builder.HasKey(ho => new { ho.HouseId, ho.personId, ho.InstituteId });

        builder.Property(ho => ho.other).HasMaxLength(500);

        builder.HasOne<House>()
            .WithMany()
            .HasForeignKey(ho => ho.HouseId)
            .HasConstraintName("FK_HouseOwners_Houses_HouseId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Person>()
            .WithMany()
            .HasForeignKey(ho => ho.personId)
            .HasConstraintName("FK_HouseOwners_Persons_personId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Institute>()
            .WithMany()
            .HasForeignKey(ho => ho.InstituteId)
            .HasConstraintName("FK_HouseOwners_Institutes_InstituteId")
            .OnDelete(DeleteBehavior.Cascade);
    }
    
}
