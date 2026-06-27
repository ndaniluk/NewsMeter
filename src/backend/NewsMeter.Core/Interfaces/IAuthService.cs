using NewsMeter.Core.DTOs;

namespace NewsMeter.Core.Interfaces;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(string email, string password);
    Task<AuthResult> LoginAsync(string email, string password);
}
