using Microsoft.AspNetCore.Identity;

namespace NewsMeter.Infrastructure.Identity;

public class AuthResult
{
    public bool Succeeded { get; init; }
    public string? Token { get; init; }
    public IEnumerable<IdentityError> Errors { get; init; } = [];

    public static AuthResult Success(string token) => new() { Succeeded = true, Token = token };
    public static AuthResult Failure(IEnumerable<IdentityError> errors) => new() { Succeeded = false, Errors = errors };
    public static AuthResult Unauthorized() => new() { Succeeded = false };
}
