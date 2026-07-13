using MIS.Domain.Entities.Geography;

namespace MIS.Application.Features.Geography.Toles;


public static class ToleDTOExtensions
{
  public static ToleDTO ToToleDTO(this Tole tole)
  {
    return new ToleDTO
    {
      Id = tole.Id,
      Code = tole.Code,
      Name = tole.Name,
      WardId = tole.WardId,
    };
  }
}



