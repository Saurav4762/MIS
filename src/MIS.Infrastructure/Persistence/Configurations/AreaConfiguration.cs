using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.Geography;

namespace MIS.Infrastructure.Persistence.Configurations;

public class AreaConfiguration : IEntityTypeConfiguration<Area>
{
	public void Configure(EntityTypeBuilder<Area> builder)
	{
		builder.HasKey(a => a.Id);

		builder.HasIndex(a => new { a.DistrictId, a.Code })
			.IsUnique();

		builder.Property(a => a.Code)
			.IsRequired();

		builder.Property(a => a.NameEn)
			.IsRequired();

		builder.Property(a => a.NameNe)
			.IsRequired();

		builder.HasOne(a => a.District)
			.WithMany(d => d.Areas)
			.HasForeignKey(a => a.DistrictId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}

