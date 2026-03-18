namespace MIS.Application.Features.Authentication;

public class AuthResultDTO
{
  public string AccessToken { get; set; } = null!;
  public DateTime ExpiresAtUtc { get; set; }
}