using GoKinoGo.DTOs.Auth;
using GoKinoGo.DTOs.User;
using GoKinoGo.Entities;

namespace GoKinoGo.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(CreateUserDto dto, UserRole userRole = UserRole.User);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
}
