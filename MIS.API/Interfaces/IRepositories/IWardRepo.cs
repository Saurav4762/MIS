using MIS.API.Models;

namespace MIS.API.Interfaces.IRepositories;

public interface IWardRepo
{
    Task<IEnumerable<Ward>> GetAllWardsAsync();
    
    Task<Ward?> GetWardByIdAsync(Guid Id);
    
    Task<Ward> CreateWardAsync(Ward ward );
    
    Task UpdateAsync ( Ward ward );
    
    Task DeleteWardById(Ward ward);
    
    Task<bool> MunicipalityExistsAsync(Guid id);
    
    Task<bool> WardExistsAsync(string name, Guid MunicipalityId);
    Task<bool>WardNameExistsAsync(string name, Guid MunicipalityId, Guid WardId);
    
    Task<Ward?> GetWardWithMunicipality(Guid id);


   
}