using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.API.Models;

namespace MIS.API.Data.Configurations;

public class Municipalityconfiguration : IEntityTypeConfiguration<Municipality>

{
    public void Configure(EntityTypeBuilder<Municipality> builder)
    {
            
    }
}