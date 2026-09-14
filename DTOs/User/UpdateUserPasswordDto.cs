using System.ComponentModel.DataAnnotations;

namespace GoKinoGo.DTOs.User;

public record UpdateUserPasswordDto
{
    [Required(ErrorMessage = "Current password is required.")]
    public required string CurrentPassword { get; init; }
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
    [Required(ErrorMessage = "Password is required.")]
    public required string NewPassword { get; init; }
}
