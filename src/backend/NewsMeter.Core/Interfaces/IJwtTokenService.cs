namespace NewsMeter.Core.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(string userId, string email);
}
