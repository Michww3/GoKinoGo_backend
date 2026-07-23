namespace GoKinoGo.DTOs.User;

public record UserDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string UserName { get; init;}
    public required string Email { get; init; }
    public required string Role { get; set; }   
}
