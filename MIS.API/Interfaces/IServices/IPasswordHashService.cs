namespace MIS.API.Interfaces.IServices;

public interface IPasswordHashService
{
  public string Hash(string password);
  public bool Verify(string password, string hash);
}