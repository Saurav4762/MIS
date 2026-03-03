

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MIS.API.Configurations;
using MIS.API.Dtos;
using MIS.API.Interfaces.IServices;
using MIS.API.Models;

namespace MIS.API.Services;

public class TokenService : ITokenService
{

    string Issuer { get; }
    string Audience { get; }
    string Key { get; }
    uint ExpiresIn { get; }
    public TokenService(IOptions<JWTSettings> JWTSettings)
    {
        Issuer = JWTSettings.Value.Issuer;
        Audience = JWTSettings.Value.Audience;
        Key = JWTSettings.Value.Key;
        ExpiresIn = JWTSettings.Value.ExpiresIn;
    }

    public Task<string>  GenerateToken(AppUser user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username!)
        };

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));

        var creds = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);

        var expires = DateTime.UtcNow.AddMinutes(ExpiresIn);

        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
    }
}