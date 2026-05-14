namespace MIS.Application.Features.Geography.Areas;

public record CreateAreaDTO
{
	public Guid DistrictId { get; set; }
	public string Code { get; set; } = null!;
	public string NameEn { get; set; } = null!;
	public string NameNe { get; set; } = null!;
}