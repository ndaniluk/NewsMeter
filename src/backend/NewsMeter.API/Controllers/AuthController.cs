using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsMeter.Infrastructure.Identity;

namespace NewsMeter.API.Controllers;

[Route("/api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthController(UserManager<AppUser> userManager, IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(AuthRequest registerRequest)
    {
        var existingUser = await _userManager.FindByEmailAsync(registerRequest.Email);
        if (existingUser != null)
        {
            return Conflict("User already exists");
        }

        var user = new AppUser()
        {
            Email = registerRequest.Email,
            UserName = registerRequest.Email
        };

        var userIdentity = await _userManager.CreateAsync(user, registerRequest.Password);

        if (userIdentity.Succeeded)
        {
            return Ok();
        }
        else
        {
            return BadRequest(userIdentity.Errors);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthRequest loginRequest)
    {
        var existingUser = await _userManager.FindByEmailAsync(loginRequest.Email);
        if (existingUser == null)
        {
            return Unauthorized();
        }

        var isUserAuthenticated = await _userManager.CheckPasswordAsync(existingUser, loginRequest.Password);

        if (isUserAuthenticated)
        {
            var token = _jwtTokenService.GenerateToken(existingUser);
            return Ok(new { token });
        }

        return Unauthorized();
    }
}
