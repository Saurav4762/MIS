using MIS.API.Models;

using MIS.API.DTOs;

namespace MIS.API.Interfaces.IRepositories;

public interface IWardRepo
{
    Task<PaginatedResponse<Ward>> GetAllWardsAsync(int pageNumber, int pageSize);
    
    Task<Ward?> GetWardByIdAsync(Guid Id);
    
    Task<Ward> CreateWardAsync(Ward ward );
    
    Task UpdateAsync ( Ward ward );
    
    Task DeleteWardById(Ward ward);
    
    Task<bool> MunicipalityExistsAsync(Guid id);

    Task<bool> WardExistsByIdAsync( Guid id);
    
    Task<bool> WardExistsAsync(string name, Guid MunicipalityId);
    Task<bool>WardNameExistsAsync(string name, Guid MunicipalityId, Guid WardId);
    
    Task<Ward?> GetWardWithMunicipality(Guid id);


   
}