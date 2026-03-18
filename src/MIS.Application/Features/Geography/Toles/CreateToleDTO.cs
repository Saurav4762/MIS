namespace MIS.Application.Features.Geography.Toles;

public class CreateToleDTO
{
	public Guid WardId { get; set; }
	public string Code { get; set; } = null!;
	public string Name { get; set; } = null!;
}
