using AutoMapper;
using GoKinoGo.DataAccess.UnitOfWork;
using GoKinoGo.DTOs.Movie;
using GoKinoGo.Entities;
using GoKinoGo.Exceptions;
using GoKinoGo.Services.Interfaces;

namespace GoKinoGo.Services;

public class MovieService(IUnitOfWork unitOfWork, IMapper mapper) : IMovieService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<MovieDto>> GetAllMoviesAsync()
    {
        var movies = await _unitOfWork.Movies.GetMoviesWithGenresAsync();
        return _mapper.Map<IEnumerable<MovieDto>>(movies);
    }

    public async Task<MovieDto?> GetMovieByIdAsync(int movieId)
    {
        var movie = await _unitOfWork.Movies.GetMovieWithGenresByIdAsync(movieId);
        return movie == null ? null : _mapper.Map<MovieDto>(movie);
    }

    public async Task<(IEnumerable<MovieDto> Movies, int TotalCount)> GetPagedMoviesAsync(
        int pageNumber,
        int pageSize,
        string? searchQuery = null)
    {
        var (movies, totalCount) = await _unitOfWork.Movies.GetPagedAsync(
            pageNumber, pageSize, searchQuery);

        var moviesDto = _mapper.Map<IEnumerable<MovieDto>>(movies);
        return (moviesDto, totalCount);
    }

    public async Task<MovieDto> CreateMovieAsync(CreateMovieDto dto)
    {
        var movie = _mapper.Map<Movie>(dto);

        if (dto.GenreIds.Count != 0)
        {
            var genreIds = dto.GenreIds.Distinct().ToList();

            var genres = await _unitOfWork.Genres.FindAsync(g => genreIds.Contains(g.Id));

            if (genreIds.Count != genres.Count())
            {
                throw new NotFoundException("One or more genre IDs are not found.");
            }
            foreach (var genre in genres)
            {
                movie.Genres.Add(genre);
            }
        }

        await _unitOfWork.Movies.AddAsync(movie);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<MovieDto>(movie);
    }

    public async Task<MovieDto?> UpdateMovieAsync(int movieId, UpdateMovieDto dto)
    {
        var movie = await _unitOfWork.Movies.GetMovieWithGenresByIdAsync(movieId)
            ?? throw new NotFoundException("Movie not found.");

        _mapper.Map(dto, movie);

        if (dto.GenreIds != null)
        {
            var genreIds = dto.GenreIds.Distinct().ToList();
            var genres = await _unitOfWork.Genres.FindAsync(g => genreIds.Contains(g.Id));
            if (genreIds.Count != genres.Count())
            {
                throw new NotFoundException("One or more genre IDs are not found.");
            }

            movie.Genres.Clear();
            foreach (var genre in genres)
            {
                movie.Genres.Add(genre);
            }
        }

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<MovieDto>(movie);
    }

    public async Task DeleteMovieAsync(int movieId)
    {
        var movie = await _unitOfWork.Movies.GetByIdAsync(movieId)
            ?? throw new NotFoundException("Movie not found.");

        _unitOfWork.Movies.Remove(movie);
        await _unitOfWork.SaveChangesAsync();
    }
}
