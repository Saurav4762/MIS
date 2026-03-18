using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MIS.Application.Features.Authentication;
using MIS.Domain.Entities.Identity;

namespace MIS.Infrastructure.Identity;

public class JwtTokenService : IJwtTokenService
{
  private readonly JwtOptions _options;

  public JwtTokenService(IOptions<JwtOptions> options)
  {
    _options = options.Value;
  }

  public AuthResultDTO GenerateToken(User user)
  {
    var now = DateTime.UtcNow;
    var expires = now.AddMinutes(_options.ExpiryMinutes);

    var claims = new List<Claim>
    {
      new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
      new(JwtRegisteredClaimNames.Email, user.Email),
      new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
      issuer: _options.Issuer,
      audience: _options.Audience,
      claims: claims,
      notBefore: now,
      expires: expires,
      signingCredentials: creds);

    return new AuthResultDTO
    {
      AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
      ExpiresAtUtc = expires
    };
  }
}