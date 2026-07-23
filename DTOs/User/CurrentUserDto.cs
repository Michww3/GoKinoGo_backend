using GoKinoGo.Entities;

namespace GoKinoGo.DTOs.User;

public record CurrentUserDto
{
    public int Id { get; init;}
    public UserRole Role { get; init;}
}
