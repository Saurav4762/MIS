using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MIS.API.Models;

public class EducationConfiguration : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> entity)
    {
        entity.HasKey(u  => u.Id);
        
        entity.HasOne(e=>e.Person)
            .WithMany(p=>p.Educations)
            .HasForeignKey(e=>e.PersonId)
            .OnDelete(DeleteBehavior.Restrict);


    } 

}


