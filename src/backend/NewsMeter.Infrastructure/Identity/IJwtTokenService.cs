namespace NewsMeter.Infrastructure.Identity;

public interface IJwtTokenService
{
    string GenerateToken(AppUser user);
}
