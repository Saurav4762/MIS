using MIS.API.Models;

namespace MIS.API.Repositories.Interfaces;

public interface IWardRepo
{
    Task<List<Ward>> GetAllWards();
    
    Task<Ward?> GetWardById(Guid Id);
    
    Task<Ward> CreateWard(string name, string code );
    
    Task<Ward>UpdateAsync ( Guid id, string name, string code);
    
    Task DeleteWardById(Guid Id);
    
   
    
}