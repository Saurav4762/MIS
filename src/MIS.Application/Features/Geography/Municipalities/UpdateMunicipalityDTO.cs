namespace MIS.Application.Features.Geography.Municipalities;

public class UpdateMunicipalityDTO
{
	public string? Code { get; set; }
	public string? NameEn { get; set; }
	public string? NameNe { get; set; }
	public string? HeadExecutiveNameEn { get; set; } = null!;
	public string? HeadExecutiveNameNe { get; set; } = null!;
	public string? Email { get; set; } = null!;
	public string? PhoneNo { get; set; } = null!;

}
