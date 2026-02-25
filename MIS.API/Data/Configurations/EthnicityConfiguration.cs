using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MIS.API.Models;


public class EthnicityConfiguration : IEntityTypeConfiguration<Ethnicity>
{
    public void Configure(EntityTypeBuilder<Ethnicity> entity)
    {
        entity.HasIndex(e=>e.Name)
            .IsUnique();
        
       
            
    }
}