namespace MIS.Application.Features.Geography.Wards;

public class CreateWardDTO
{
	public Guid MunicipalityId { get; set; }
	public string Code { get; set; } = null!;
	public string Name { get; set; } = null!;
}
