using FluentValidation;
using MIS.Application.Common.Extensions;
using MIS.Domain.Entities.DataCollection.HouseInfo;

namespace MIS.Application.Features.DataCollection.HouseInfo;

public class HouseInfoService : IHouseInfoService
{
  private readonly IHouseInfoRepo _houseRepo;
  private readonly IValidator<CreateHouseInfoDTO> _validator;

  public HouseInfoService(IHouseInfoRepo houseRepo, IValidator<CreateHouseInfoDTO> validator)
  {
    _houseRepo = houseRepo;
    _validator = validator;
  }
  public async Task CreateHouseAsync(CreateHouseInfoDTO house)
  {
    await _validator.EnsureValidOrThrowAsync(house);

    // check submission exists
    // check ward and tole exists and are related



    await _houseRepo.CreateHouseAsync(new House
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
    });
  }

  public async Task<HouseInfoDTO> GetHouseByIdAsync(Guid id)
  {
    throw new NotImplementedException();
  }

}