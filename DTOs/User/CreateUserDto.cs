using System.ComponentModel.DataAnnotations;

namespace GoKinoGo.DTOs.User;

public class CreateUserDto
{
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string UserName { get; set; } = string.Empty;
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;
}
