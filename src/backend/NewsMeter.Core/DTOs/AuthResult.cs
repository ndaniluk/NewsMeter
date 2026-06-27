namespace NewsMeter.Core.DTOs;

public class AuthResult
{
    public bool Succeeded { get; init; }
    public string? Token { get; init; }
    public IEnumerable<string> Errors { get; init; } = [];

    public static AuthResult Success(string token) => new() { Succeeded = true, Token = token };
    public static AuthResult Failure(IEnumerable<string> errors) => new() { Succeeded = false, Errors = errors };
    public static AuthResult Unauthorized() => new() { Succeeded = false };
}
