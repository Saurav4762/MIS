using MIS.API.Dtos;
using MIS.API.Models;

namespace MIS.API.Repositories.Interfaces;

public interface IReligionRepo
{
    Task<Religion> GetReligionByIdAsync(Guid id);

    Task<List<Religion>> GetReligionsAsync();
    
    Task<Religion> AddReligionAsync(string nameEn, string nameNe);
    
    Task<Religion> UpdateReligionAsync(Guid id, string nameEn, string nameNe  );
    
    Task DeleteReligionAsync(Guid id);
}