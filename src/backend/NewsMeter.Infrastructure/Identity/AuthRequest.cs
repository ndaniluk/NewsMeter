using System.ComponentModel.DataAnnotations;

namespace NewsMeter.Infrastructure.Identity;

public class AuthRequest
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [MinLength(8)]
    public required string Password { get; set; }
}