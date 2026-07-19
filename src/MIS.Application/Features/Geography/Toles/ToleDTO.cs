namespace MIS.Application.Features.Geography.Toles;

public class ToleDTO
{
  public Guid Id { get; set; }
  public string Code { get; set; } = null!;
  public string Name { get; set; } = null!;
  public Guid WardId { get; set; }
}

