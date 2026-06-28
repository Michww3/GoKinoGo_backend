using GoKinoGo.Data;
using GoKinoGo.DataAccess.Repositories.Interfaces;
using GoKinoGo.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoKinoGo.DataAccess.Repositories;

public class MovieRepository(AppDbContext context) : Repository<Movie>(context), IMovieRepository
{
    public async Task<Movie?> GetFullMovieByIdAsync(int movieId)
    {
        return await _context.Movies
            .Include(m => m.Genres)
            .Include(m => m.Comments)
            .ThenInclude(c => c.Owner)
            .Include(m => m.Comments)
            .ThenInclude(c => c.Likes)
            .ThenInclude(l => l.User)
            .SingleOrDefaultAsync(m => m.Id == movieId);
    }

    public async Task<IEnumerable<Movie>> GetMoviesWithGenresAsync()
    {
        return await _dbSet
            .Include(m => m.Genres)
            .ToListAsync();
    }

    public async Task<Movie?> GetMovieWithGenresByIdAsync(int movieId)
    {
        return await _dbSet
            .Include(m => m.Genres)
            .SingleOrDefaultAsync(m => m.Id == movieId);
    }

    public async Task<(IEnumerable<Movie> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, string? searchQuery = null)
    {
        var query = _context.Movies
                 .Include(m => m.Genres)
                 .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            query = query.Where(m => m.Name.Contains(searchQuery));
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(m => m.ReleaseDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
}
