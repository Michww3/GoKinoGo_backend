using GoKinoGo.Data;
using GoKinoGo.DataAccess.Repositories.Interfaces;
using GoKinoGo.DTOs.Genre;
using GoKinoGo.DTOs.Movie;
using GoKinoGo.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoKinoGo.DataAccess.Repositories;

public class MovieRepository(AppDbContext context) : Repository<Movie>(context), IMovieRepository
{
    public async Task<MovieDetailsDto?> GetMovieDetailsByIdAsync(int movieId, int? userId)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(m => m.Id == movieId)
            .Select(m => new MovieDetailsDto
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                Price = m.Price,
                ReleaseDate = m.ReleaseDate,
                Length = m.Length,
                PosterUrl = m.PosterUrl,

                Genres = m.Genres
                    .Select(g => new GenreDto
                    {
                        Id = g.Id,
                        Name = g.Name
                    })
                    .ToList(),

                AverageRating = m.MovieRatings
                    .Select(r => (double?)r.Value)
                    .Average() ?? 0,

                RatingsCount = m.MovieRatings.Count,

                UserRating = userId == null
                    ? null
                    : m.MovieRatings
                        .Where(r => r.UserId == userId)
                        .Select(r => (int?)r.Value)
                        .FirstOrDefault()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<MovieCardDto>> GetAllMoviesAsync()
    {
        return await _dbSet
            .AsNoTracking()
            .Select(m => new MovieCardDto
            {
                Id = m.Id,
                Name = m.Name,
                Price = m.Price,
                PosterUrl = m.PosterUrl,
                ReleaseDate = m.ReleaseDate,

                Genres = m.Genres
                    .Select(g => new GenreDto
                    {
                        Id = g.Id,
                        Name = g.Name
                    })
                    .ToList(),

                AverageRating = m.MovieRatings
                    .Select(r => (double?)r.Value)
                    .Average() ?? 0
            })
            .ToListAsync();
    }

    public async Task<(IEnumerable<MovieCardDto> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, string? searchQuery = null)
    {
        throw new NotImplementedException();
    }

    public async Task<Movie?> GetTrackedByIdAsync(int movieId)
    {
        return await _dbSet
            .Include(m => m.Genres)
            .FirstOrDefaultAsync(m => m.Id == movieId);
    }
}
