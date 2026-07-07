using GoKinoGo.DTOs.Movie;

namespace GoKinoGo.Services.Interfaces;

public interface IMovieService
{
    Task<IEnumerable<MovieDto>> GetAllMoviesAsync();
    Task<MovieDto?> GetMovieByIdAsync(int movieId);
    Task<(IEnumerable<MovieDto> Movies, int TotalCount)> GetPagedMoviesAsync(
        int pageNumber,
        int pageSize,
        string? searchQuery = null);
    Task<MovieDto> CreateMovieAsync(CreateMovieDto dto);
    Task<MovieDto?> UpdateMovieAsync(int movieId, UpdateMovieDto dto);
    Task DeleteMovieAsync(int movieId);
}
