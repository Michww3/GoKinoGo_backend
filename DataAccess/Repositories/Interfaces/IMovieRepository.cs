using GoKinoGo.DTOs.Movie;
using GoKinoGo.Entities;

namespace GoKinoGo.DataAccess.Repositories.Interfaces;

public interface IMovieRepository : IRepository<Movie>
{
    Task<Movie?> GetTrackedByIdAsync(int movieId);
    Task<IEnumerable<MovieCardDto>> GetAllMoviesAsync();
    Task<MovieDetailsDto?> GetMovieDetailsByIdAsync(int movieId, int? userId);
    Task<(IEnumerable<MovieCardDto> Items, int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        string? searchQuery = null);
}
