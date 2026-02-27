using MIS.API.Dtos;
using MIS.API.Models;

namespace MIS.API.Repositories;

public interface IReligionRepo
{
    Task<Religion> GetReligionByIdAsync(Guid id);

    Task<List<Religion>> GetReligionsAsync();
    
    Task<Religion> AddReligionAsync(ReligionRequestDto dtos);
    
    Task<Religion> UpdateReligionAsync(Guid id, ReligionRequestDto religion);
    
    Task DeleteReligionAsync(Guid id);
}