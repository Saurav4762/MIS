using MIS.API.Models;

namespace MIS.API.Repositories.Interfaces;

public interface IAppRoleRepo
{
    
    Task<AppRole> CreateAsync(AppRole role);
    Task<AppRole> GetRoleById(Guid id);
    Task DeleteAsync(Guid id);
}