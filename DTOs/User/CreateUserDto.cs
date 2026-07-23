using System.ComponentModel.DataAnnotations;

namespace GoKinoGo.DTOs.User;

public record CreateUserDto
{
    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
    public required string Name { get; init; }
    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 50 characters.")]
    public required string UserName { get; init; }
    [Required]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
    public required string Password { get; init; }
    [Required]
    [EmailAddress]
    [StringLength(100, ErrorMessage = "Email must be between 1 and 100 characters.")]
    public required string Email { get; init; }
}
