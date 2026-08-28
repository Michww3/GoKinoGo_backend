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

    public async Task<(IEnumerable<MovieCardDto> Items, int TotalCount)> GetPagedAsync(MoviesQuery query)
    {
        var movies = _dbSet.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.SearchQuery))
        {
            var searchQuery = query.SearchQuery.Trim().ToLower();
            movies = movies.Where(m => m.Name.ToLower().Contains(searchQuery));
        }

        if(query.GenreIds != null && query.GenreIds!.Length > 0)
        {
            movies = movies.Where(m => m.Genres.Any(g => query.GenreIds.Contains(g.Id)));
        }

        if (query.MinPrice.HasValue)
        {
            movies = movies.Where(m =>
                m.Price >= query.MinPrice.Value);
        }

        if (query.MaxPrice.HasValue)
        {
            movies = movies.Where(m =>
                m.Price <= query.MaxPrice.Value);
        }

        if (query.MinYear.HasValue)
        {
            movies = movies.Where(m =>
                m.ReleaseDate.Year >= query.MinYear.Value);
        }

        if (query.MaxYear.HasValue)
        {
            movies = movies.Where(m =>
                m.ReleaseDate.Year <= query.MaxYear.Value);
        }

        if (query.MinRating.HasValue)
        {
            movies = movies.Where(m =>
                m.MovieRatings.Any() &&
                m.MovieRatings.Average(r => r.Value) >= query.MinRating.Value);
        }

        movies = query.SortBy switch
        {
            "dateAsc" =>
                movies.OrderBy(m => m.ReleaseDate),

            "dateDesc" =>
                movies.OrderByDescending(m => m.ReleaseDate),

            "ratingDesc" =>
                movies.OrderByDescending(m =>
                    m.MovieRatings
                        .Select(r => (double?)r.Value)
                        .Average() ?? 0),

            "recentlyAdded" => 
            movies.OrderByDescending(m => m.Id),
                
            _ =>
                movies.OrderByDescending(m => m.Id)
        };

        var totalCount = await movies.CountAsync();

        var items = await movies
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
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

        return(items, totalCount);
    }

    public async Task<Movie?> GetTrackedByIdAsync(int movieId)
    {
        return await _dbSet
            .Include(m => m.Genres)
            .FirstOrDefaultAsync(m => m.Id == movieId);
    }
}
