namespace MIS.Application.Features.DataCollection.HouseInfo;

public interface IHouseService
{
  Task CreateHouseAsync(CreateHouseDTO house);
  Task<HouseDTO> GetHouseByIdAsync(Guid id);
}