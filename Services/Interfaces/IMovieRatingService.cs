using GoKinoGo.DTOs.MovieRating;
using GoKinoGo.DTOs.User;

namespace GoKinoGo.Services.Interfaces;

public interface IMovieRatingService
{
    Task RateAsync(int movieId, int userId, int value);
    Task DeleteRatingAsync(int movieId, CurrentUserDto currentUser);
}
