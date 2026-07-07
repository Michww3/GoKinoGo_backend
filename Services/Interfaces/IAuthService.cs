using GoKinoGo.DTOs.Auth;
using GoKinoGo.DTOs.User;

namespace GoKinoGo.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(CreateUserDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
}
