using GoKinoGo.Constants;
using GoKinoGo.DataAccess.UnitOfWork;
using GoKinoGo.DTOs.User;
using GoKinoGo.Entities;
using GoKinoGo.Exceptions;
using GoKinoGo.Services.Interfaces;

namespace GoKinoGo.Services;

public class MovieRatingService(IUnitOfWork unitOfWork) : IMovieRatingService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task RateAsync(int movieId, int userId, int value)
    {
        _ = await _unitOfWork.Movies.GetByIdAsync(movieId)
            ?? throw new NotFoundException(ErrorMessages.Movie.NotFound);

        var rating = await _unitOfWork.MovieRatings
            .GetByMovieAndUserAsync(movieId, userId);

        if (rating is null)
        {
            await _unitOfWork.MovieRatings.AddAsync(new MovieRating
            {
                MovieId = movieId,
                UserId = userId,
                Value = value
            });
        }
        else
        {
            rating.Value = value;
        }

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteRatingAsync(int movieId, int ratingId, CurrentUserDto currentUser)
    {
        _ = await _unitOfWork.Movies.GetByIdAsync(movieId)
            ?? throw new NotFoundException(ErrorMessages.Movie.NotFound);

        var rating = await _unitOfWork.MovieRatings.GetByIdAsync(ratingId)
            ?? throw new NotFoundException(ErrorMessages.MovieRating.NotFound);

        if (rating.MovieId != movieId)
            throw new NotFoundException(ErrorMessages.MovieRating.NotFound);

        if (rating.UserId != currentUser.Id && currentUser.Role != UserRole.Admin)
            throw new ForbiddenException(ErrorMessages.MovieRating.CannotDeleteOtherRating);

        _unitOfWork.MovieRatings.Remove(rating);

        await _unitOfWork.SaveChangesAsync();
    }
}
