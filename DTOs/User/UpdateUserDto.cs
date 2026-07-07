using System.ComponentModel.DataAnnotations;

namespace GoKinoGo.DTOs.User;

public class UpdateUserDto
{
    [StringLength(50, MinimumLength = 2)]
    public string? Name { get; set; }
    [StringLength(50, MinimumLength = 2)]
    public string? UserName { get; set; }
    [StringLength(100, MinimumLength = 6)]
    public string? Password { get; set; }
    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string? Email { get; set; }
}
