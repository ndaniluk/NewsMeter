using Microsoft.AspNetCore.Mvc;
using NewsMeter.API.Requests;
using NewsMeter.Infrastructure.Identity;

namespace NewsMeter.API.Controllers;

[Route("/api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(AuthRequest request)
    {
        var result = await _authService.RegisterAsync(request.Email, request.Password);

        if (result.Succeeded)
        {
            return Ok(new { result.Token });
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthRequest request)
    {
        var result = await _authService.LoginAsync(request.Email, request.Password);

        if (result.Succeeded)
        {
            return Ok(new { result.Token });
        }

        return Unauthorized();
    }
}
