
using MIS.Domain.Entities.DataCollection.HouseInfo;

namespace MIS.Application.Features.DataCollection.HouseInfo;

public static class ToHouseInfoDTO
{
  public static HouseInfoDTO ToDTO(House house)
  {
    return new HouseInfoDTO
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
