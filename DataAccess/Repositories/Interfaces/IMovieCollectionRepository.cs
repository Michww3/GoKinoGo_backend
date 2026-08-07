using GoKinoGo.DTOs.MovieCollection;
using GoKinoGo.Entities;

namespace GoKinoGo.DataAccess.Repositories.Interfaces;

public interface IMovieCollectionRepository : IRepository<MovieCollection>
{
    Task<IEnumerable<Movie>> GetMoviesAsync(int collectionId);
    Task<bool> ExistsByNameAsync(string name);
    Task<MovieCollection?> GetByIdWithMoviesAsync(int id);
}
