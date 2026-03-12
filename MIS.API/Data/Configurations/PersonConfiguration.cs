using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.API.Models;
using MIS.API.Enum;

namespace MIS.API.Data.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p=>p.FirstName).IsRequired();
        builder.Property(p=>p.MiddleName).IsRequired();
        builder.Property(p=>p.LastName).IsRequired();
        builder.Property(p => p.Email).IsRequired();
        builder.Property(p=>p.Gender)
            .HasConversion<String>()
            .IsRequired();
        builder.Property(p => p.DateOfBirth).IsRequired();
        builder.Property(p=>p.BloodGroup).IsRequired();
        builder.Property(p=>p.DateOfBirth).IsRequired();
        
        
        //one to many with HouseOwners
        builder.HasMany(x => x.HouseOwners)
            .WithOne(h=>h.Person)
            .HasForeignKey(h=>h.PersonId)
            .OnDelete(DeleteBehavior.Cascade);
        
        //one to Many with Education 
        builder.HasMany(p=>p.Educations)
            .WithOne(e=>e.Person)
            .HasForeignKey(e=>e.PersonId)
            .OnDelete(DeleteBehavior.Cascade);
        
        //one to one with Family
        builder.HasOne(p => p.Family)
            .WithOne(f=>f.HeadOfTheFamily)
            .HasForeignKey<Family>(f=>f.HeadOfTheFamilyId)
            .OnDelete(DeleteBehavior.Cascade);
        
        
    }
}