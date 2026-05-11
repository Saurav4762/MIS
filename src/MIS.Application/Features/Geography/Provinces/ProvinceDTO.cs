namespace MIS.Application.Features.Geography.Provinces;

public class ProvinceDTO
{
	public Guid Id { get; set; }
	public string Code { get; set; } = null!;
	public string NameEn { get; set; } = null!;
	public string NameNe { get; set; } = null!;
}
