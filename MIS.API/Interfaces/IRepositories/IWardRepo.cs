using MIS.API.Models;

namespace MIS.API.Interfaces.IRepositories;

public interface IWardRepo
{
    Task<List<Ward>> GetAllWardsAsync();
    
    Task<Ward?> GetWardByIdAsync(Guid Id);
    
    Task<Ward> CreateWardAsync(string name, string code, Guid municipalityId );
    
    Task<Ward>UpdateAsync ( Guid id, string name, string code);
    
    Task DeleteWardById(Guid Id);


   
}