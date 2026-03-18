namespace MIS.Application.Features.Authentication;

public interface IPasswordHashService
{
  public string Hash(string password);
  public bool Verify(string password, string hash);
}