using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NewsMeter.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AppJwtPayload = NewsMeter.Infrastructure.Identity.JwtPayload;

namespace NewsMeter.Infrastructure.Identity;

public class JwtTokenService : IJwtTokenService
{
    private readonly AppJwtPayload _jwtPayload;

    public JwtTokenService(IOptions<AppJwtPayload> jwtPayload)
    {
        _jwtPayload = jwtPayload.Value;
    }

    public string GenerateToken(string userId, string email)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtPayload.Key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(_jwtPayload.Issuer, _jwtPayload.Audience, claims, null,
            DateTime.UtcNow.AddMinutes(_jwtPayload.ExpiryMinutes), signingCredentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
