using GoKinoGo.DTOs.Genre;

namespace GoKinoGo.Services.Interfaces;

public interface IGenreService
{
    Task<IEnumerable<GenreDto>> GetAllGenresAsync();
    Task<GenreDto> GetGenreByIdAsync(int genreId);
    Task<GenreDto> CreateGenreAsync(CreateGenreDto dto);
    Task<GenreDto> UpdateGenreAsync(int genreId, UpdateGenreDto dto);
    Task DeleteGenreAsync(int genreId);
}
