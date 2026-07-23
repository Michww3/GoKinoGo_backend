using GoKinoGo.DTOs.User;

namespace GoKinoGo.DTOs.Auth;

public record AuthResponseDto
{
    public required string Token { get; init; }
    public required UserDto User { get; init; }
}
