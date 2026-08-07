using GoKinoGo.DTOs.Movie;
using GoKinoGo.DTOs.MovieCollection;

namespace GoKinoGo.Services.Interfaces;

public interface IMovieCollectionService
{
    Task<MovieCollectionDto> CreateAsync(CreateMovieCollectionDto dto);
    Task<IEnumerable<MovieCollectionDto>> GetAllAsync();
    Task<MovieCollectionDto> GetByIdAsync(int id);
    Task<IEnumerable<MovieDto>> GetMoviesAsync(int collectionId);
    Task<MovieCollectionDto> UpdateAsync(int id, UpdateMovieCollectionDto dto);
    Task DeleteAsync(int id);
}
