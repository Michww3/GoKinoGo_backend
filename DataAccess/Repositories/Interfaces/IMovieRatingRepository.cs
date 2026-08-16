using GoKinoGo.DTOs.MovieRating;
using GoKinoGo.Entities;

namespace GoKinoGo.DataAccess.Repositories.Interfaces;

public interface IMovieRatingRepository : IRepository<MovieRating>
{
    public Task<MovieRatingStats> GetMovieStatsAsync(int movieId);
    public Task<MovieRating?> GetByMovieAndUserAsync(int movieId, int userId);
    public Task<IEnumerable<MovieRating>> GetByUserAsync(int userId);
}
