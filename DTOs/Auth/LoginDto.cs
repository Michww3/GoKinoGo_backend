using System.ComponentModel.DataAnnotations;

namespace GoKinoGo.DTOs.Auth;

public record LoginDto
{
    [Required]
    [EmailAddress]
    [StringLength(100, ErrorMessage = "Email must be between 1 and 100 characters.")]
    public required string Email { get; init; }
    [Required]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
    public required string Password { get; init; }
}
