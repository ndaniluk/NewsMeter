using Microsoft.AspNetCore.Identity;

namespace NewsMeter.Infrastructure.Identity;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(UserManager<AppUser> userManager, IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<AuthResult> RegisterAsync(string email, string password)
    {
        var existingUser = await _userManager.FindByEmailAsync(email);
        if (existingUser != null)
        {
            return AuthResult.Failure([new IdentityError { Code = "DuplicateEmail", Description = "User already exists" }]);
        }

        var user = new AppUser { Email = email, UserName = email };
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return AuthResult.Failure(result.Errors);
        }

        var token = _jwtTokenService.GenerateToken(user);
        return AuthResult.Success(token);
    }

    public async Task<AuthResult> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return AuthResult.Unauthorized();
        }

        var isValid = await _userManager.CheckPasswordAsync(user, password);
        if (!isValid)
        {
            return AuthResult.Unauthorized();
        }

        var token = _jwtTokenService.GenerateToken(user);
        return AuthResult.Success(token);
    }
}
