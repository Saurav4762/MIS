using MIS.API.Interfaces.IServices;

namespace MIS.API.Services;

public class PasswordHashService : IPasswordHashService
{
  public string Hash(string password)
  {
    return BCrypt.Net.BCrypt.HashPassword(inputKey: password);
  }

  public bool Verify(string password, string hash)
  {
    return BCrypt.Net.BCrypt.Verify(password, hash);
  }
}