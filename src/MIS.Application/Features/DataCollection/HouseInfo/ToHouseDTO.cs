
using MIS.Domain.Entities.DataCollection.HouseInfo;

namespace MIS.Application.Features.DataCollection.HouseInfo;

public static class ToHouseDTO
{
  public static HouseDTO ToDTO(House house)
  {
    return new HouseDTO
    {
      SubmissionId = house.SubmissionId,
      WardId = house.WardId,
      ToleId = house.ToleId,
      HouseNumber = house.HouseNumber,
      Location = house.Location,
      ImageId = house.ImageId,
      HouseTypeId = house.HouseTypeId,
      LandTypeId = house.LandTypeId,
      RoofTypeId = house.RoofTypeId,
      WallTypeId = house.WallTypeId
    };
  }
}
