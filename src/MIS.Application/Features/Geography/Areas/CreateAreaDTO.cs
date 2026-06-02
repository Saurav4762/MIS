namespace MIS.Application.Features.Geography.Areas;

public record CreateAreaDTO
{
	public Guid DistrictId { get; set; }
	public int Number { get; set; } = 0;
}