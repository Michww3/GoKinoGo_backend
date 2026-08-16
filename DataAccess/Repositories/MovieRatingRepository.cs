using GoKinoGo.Data;
using GoKinoGo.DataAccess.Repositories.Interfaces;
using GoKinoGo.DTOs.MovieRating;
using GoKinoGo.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoKinoGo.DataAccess.Repositories;

public class MovieRatingRepository(AppDbContext context) : Repository<MovieRating>(context), IMovieRatingRepository
{
    public async Task<MovieRatingStats> GetMovieStatsAsync(int movieId)
    {
        var stats = await _dbSet
            .Where(r => r.MovieId == movieId)
            .GroupBy(_ => 1)
            .Select(g => new MovieRatingStats(
                g.Average(r => r.Value),
                g.Count()))
            .FirstOrDefaultAsync();

        return stats ?? new MovieRatingStats(0, 0);
    }

    public async Task<MovieRating?> GetByMovieAndUserAsync(int movieId, int userId)
    {
        return await _dbSet
            .SingleOrDefaultAsync(r => r.MovieId == movieId && r.UserId == userId);
    }

    public async Task<IEnumerable<MovieRating>> GetByUserAsync(int userId)
    {
        return await _dbSet
            .Where(r => r.UserId == userId)
            .Include(r => r.Movie)
            .ThenInclude(m => m.Genres)
            .ToListAsync();
    }
}

