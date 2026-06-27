using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NewsMeter.Infrastructure.Identity;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtPayload _jwtPayload;

    public JwtTokenService(IOptions<JwtPayload> jwtPayload)
    {
        _jwtPayload = jwtPayload.Value;
    }

    public string GenerateToken(AppUser user)
    {
        if (_jwtPayload is null)
        {
            throw new InvalidOperationException("JWT configuration is missing");
        }

        if (string.IsNullOrEmpty(user.Email))
        {
            throw new InvalidOperationException("User email is required to generate token");
        }

        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtPayload.Key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(_jwtPayload.Issuer, _jwtPayload.Audience, claims, null, DateTime.UtcNow.AddMinutes(_jwtPayload.ExpiryMinutes), signingCredentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
