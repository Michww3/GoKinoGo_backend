using GoKinoGo.Data;
using GoKinoGo.DataAccess.Repositories.Interfaces;
using GoKinoGo.DTOs.MovieCollection;
using GoKinoGo.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoKinoGo.DataAccess.Repositories;

public class MovieCollectionRepository(AppDbContext context) : Repository<MovieCollection>(context), IMovieCollectionRepository
{
    public async Task<IEnumerable<Movie>> GetMoviesAsync(int collectionId)
    {
        return await _context.CollectionItems
                    .Where(x =>
                        x.CollectionId == collectionId &&
                        x.Collection.IsActive)
                    .Include(x => x.Movie)
                    .ThenInclude(m => m.Genres)
                    .OrderBy(x => x.Position)
                    .Select(x => x.Movie)
                    .ToListAsync();
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _dbSet.AnyAsync(mc => mc.Name == name);
    }

    public async Task<MovieCollection?> GetByIdWithMoviesAsync(int id)
    {
        return await _dbSet
                    .Include(mc => mc.Items
                        .OrderBy(ci => ci.Position))
                    .ThenInclude(x => x.Movie)
                    .ThenInclude(m => m.Genres)
                    .FirstOrDefaultAsync(mc => mc.Id == id);
    }
}
