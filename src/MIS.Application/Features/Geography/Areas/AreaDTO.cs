namespace MIS.Application.Features.Geography.Areas;

public record AreaDTO
{
	public Guid Id { get; set; }
	public Guid DistrictId { get; set; }
	public int Number { get; set; } = 0;
}