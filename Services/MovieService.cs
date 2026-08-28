using AutoMapper;
using GoKinoGo.Constants;
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

    public async Task<IEnumerable<MovieCardDto>> GetAllMoviesAsync()
    {
        return await _unitOfWork.Movies.GetAllMoviesAsync();
    }

    public async Task<MovieDetailsDto> GetMovieDetailsByIdAsync(int movieId, int? userId)
    {
        var movie = await _unitOfWork.Movies.GetMovieDetailsByIdAsync(movieId, userId)
            ?? throw new NotFoundException(ErrorMessages.Movie.NotFound);
        return movie;
    }

    public async Task<(IEnumerable<MovieCardDto> Movies, int TotalCount)> GetPagedMoviesAsync(MoviesQuery query)
    {
        return await _unitOfWork.Movies.GetPagedAsync(query);
    }

    public async Task<MovieDto> CreateMovieAsync(CreateMovieDto dto)
    {
        var movie = _mapper.Map<Movie>(dto);

        if (dto.GenreIds.Count != 0)
        {
            var genreIds = dto.GenreIds.Distinct().ToList();

            var genres = (await _unitOfWork.Genres.FindAsync(g => genreIds.Contains(g.Id))).ToList();

            if (genreIds.Count != genres.Count())
            {
                throw new NotFoundException(ErrorMessages.Movie.GenreNotFound);
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

    public async Task<MovieDto> UpdateMovieAsync(int movieId, UpdateMovieDto dto)
    {
        var movie = await _unitOfWork.Movies.GetTrackedByIdAsync(movieId)
            ?? throw new NotFoundException(ErrorMessages.Movie.NotFound);

        _mapper.Map(dto, movie);

        if (dto.GenreIds != null)
        {
            var genreIds = dto.GenreIds.Distinct().ToList();
            var genres = await _unitOfWork.Genres.FindAsync(g => genreIds.Contains(g.Id));
            if (genreIds.Count != genres.Count())
            {
                throw new NotFoundException(ErrorMessages.Movie.GenreNotFound);
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
            ?? throw new NotFoundException(ErrorMessages.Movie.NotFound);

        _unitOfWork.Movies.Remove(movie);
        await _unitOfWork.SaveChangesAsync();
    }
}
