namespace NewsMeter.Infrastructure.Identity;

public class JwtPayload
{
    public required string Key { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public int ExpiryMinutes { get; set; }
}
