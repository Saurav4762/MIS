namespace MIS.Application.Features.Geography.Municipalities;

public record CreateMunicipalityDTO
{

  public string Code { get; set; } = null!;
	public string NameEn { get; set; } = null!;
	public string NameNe { get; set; } = null!;
	public string HeadExecutiveNameEn { get; set; } = null!;
	public string HeadExecutiveNameNe { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string PhoneNo { get; set; } = null!;

}
