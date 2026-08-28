using GoKinoGo.DTOs.Movie;

namespace GoKinoGo.Services.Interfaces;

public interface IMovieService
{
    Task<IEnumerable<MovieCardDto>> GetAllMoviesAsync();
    Task<MovieDetailsDto> GetMovieDetailsByIdAsync(int movieId, int? userId);
    Task<(IEnumerable<MovieCardDto> Movies, int TotalCount)> GetPagedMoviesAsync(MoviesQuery query);
    Task<MovieDto> CreateMovieAsync(CreateMovieDto dto);
    Task<MovieDto> UpdateMovieAsync(int movieId, UpdateMovieDto dto);
    Task DeleteMovieAsync(int movieId);
}
