using MIS.API.Models;

namespace MIS.API.Interfaces.IServices;


public interface ITokenService
{
  public Task<string> GenerateToken(AppUser user);
}