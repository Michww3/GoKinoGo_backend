using System.ComponentModel.DataAnnotations;

namespace GoKinoGo.DTOs.Auth;

public record ConfirmEmailDto
{
    [Required]
    public required string Token { get; init; }
}
