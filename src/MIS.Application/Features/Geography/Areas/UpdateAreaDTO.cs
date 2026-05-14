namespace MIS.Application.Features.Geography.Areas;

public class UpdateAreaDTO
{
	public Guid? DistrictId { get; set; }
	public string? Code { get; set; }
	public string? NameEn { get; set; }
	public string? NameNe { get; set; }
}