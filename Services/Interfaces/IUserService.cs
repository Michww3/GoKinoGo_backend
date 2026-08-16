using GoKinoGo.DTOs.MovieRating;
using GoKinoGo.DTOs.User;

namespace GoKinoGo.Services.Interfaces;

public interface IUserService
{
    Task<UserDto> GetUserByIdAsync(int userId);
    Task<UserDto> GetUserByEmailAsync(string email);
    Task<UserDto> UpdateUserAsync(int userId, UpdateUserDto dto, CurrentUserDto currentUser);
    Task DeleteUserAsync(int userId, CurrentUserDto currentUser);
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByUserNameAsync(string userName);
    Task<IEnumerable<MovieRatingDto>> GetUserRatings(int userId);
}
