namespace MIS.Application.Features.Geography.Districts;

public record CreateDistrictDTO
{
	public Guid ProvinceId { get; set; }
	public string Code { get; set; } = null!;
	public string NameEn { get; set; } = null!;
	public string NameNe { get; set; } = null!;
}
