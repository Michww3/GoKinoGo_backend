using GoKinoGo.Entities;

namespace GoKinoGo.DTOs.User;

public class CurrentUserDto
{
    public int Id { get; set; }
    public UserRole Role { get; set; }
}
