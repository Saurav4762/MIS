namespace MIS.Application.Features.DataCollection.HouseInfo;

public interface IHouseInfoService
{
  Task CreateHouseAsync(CreateHouseInfoDTO house);
  Task<HouseInfoDTO> GetHouseByIdAsync(Guid id);
}