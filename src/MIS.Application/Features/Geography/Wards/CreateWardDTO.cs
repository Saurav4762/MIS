namespace MIS.Application.Features.Geography.Wards;

public class CreateWardDTO
{
	public Guid MunicipalityId { get; set; }
	public int Number { get; set; }
	public string RepresentativeNameEn { get; set; } = string.Empty;
	public string RepresentativeNameNe { get; set; } = string.Empty;
	public string PhoneNo { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
}
