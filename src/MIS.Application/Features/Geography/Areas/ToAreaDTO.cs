namespace MIS.Application.Features.Geography.Areas;

public static class ToDTO
{
  public static AreaDTO ToAreaDTO(this Domain.Entities.Geography.Area area)
  {
    return new AreaDTO
    {
      Id = area.Id,
      DistrictId = area.DistrictId,
      Number = area.Number
    };
  }

}