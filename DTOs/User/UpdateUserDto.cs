using System.ComponentModel.DataAnnotations;

namespace GoKinoGo.DTOs.User;

public record UpdateUserDto
{
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
    public string? Name { get; init;}
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 50 characters.")]
    public string? UserName { get; init;}
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
    public string? Password { get; init;}
    [EmailAddress]
    [StringLength(100, ErrorMessage = "Email must be between 1 and 100 characters.")]
    public string? Email { get; init;}
}
