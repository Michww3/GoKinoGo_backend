using GoKinoGo.Entities;

namespace GoKinoGo.DataAccess.Repositories.Interfaces;

public interface IMovieRepository : IRepository<Movie>
{
    Task<IEnumerable<Movie>> GetMoviesWithGenresAsync();
    Task<Movie?> GetMovieWithGenresByIdAsync(int movieId);
    Task<Movie?> GetFullMovieByIdAsync(int movieId);
    Task<(IEnumerable<Movie> Items, int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        string? searchQuery = null);
}
