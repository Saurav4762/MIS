using MIS.API.Models;

namespace MIS.API.Interfaces.IRepositories;

public interface IAppRoleRepo
{
    
    Task<AppRole> CreateAsync(AppRole role);
    Task<AppRole> GetRoleById(Guid id);
    Task DeleteAsync(Guid id);
}