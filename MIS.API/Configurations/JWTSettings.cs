namespace MIS.API.Configurations;


public class JWTSettings
{
  public string Issuer { get; set; } = null!;
  public string Audience { get; set; } = null!;
  public string Key { get; set; } = null!;
  public uint ExpiresIn { get; set; } = default;
}