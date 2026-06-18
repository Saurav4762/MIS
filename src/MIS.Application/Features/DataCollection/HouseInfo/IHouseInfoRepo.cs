
using MIS.Domain.Entities.DataCollection.HouseInfo;

namespace MIS.Application.Features.DataCollection.HouseInfo;

public interface IHouseInfoRepo
{
  Task CreateHouseAsync(House house);
  Task<House?> GetHouseByIdAsync(Guid id);
}