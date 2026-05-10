namespace MIS.Application.Features.Geography.Districts;

public class UpdateDistrictDTO
{
	public Guid? ProvinceId { get; set; }
	public string? Code { get; set; }
	public string? NameEn { get; set; }
	public string? NameNe { get; set; }
}
