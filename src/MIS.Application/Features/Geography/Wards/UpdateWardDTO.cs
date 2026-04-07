namespace MIS.Application.Features.Geography.Wards;

public class UpdateWardDTO
{
	public Guid? MunicipalityId { get; set; }
	public int? Number { get; set; }
	public string? RepresentativeNameEn { get; set; }
	public string? RepresentativeNameNe { get; set; }
	public string? PhoneNo { get; set; }
	public string? Email { get; set; }
}
