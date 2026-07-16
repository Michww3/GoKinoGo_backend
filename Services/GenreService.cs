using AutoMapper;
using GoKinoGo.Constants;
using GoKinoGo.DataAccess.UnitOfWork;
using GoKinoGo.DTOs.Genre;
using GoKinoGo.Entities;
using GoKinoGo.Exceptions;
using GoKinoGo.Services.Interfaces;

namespace GoKinoGo.Services;

public class GenreService(IUnitOfWork unitOfWork, IMapper mapper) : IGenreService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<GenreDto> CreateGenreAsync(CreateGenreDto dto)
    {
        var exists = await _unitOfWork.Genres
            .ExistByNameAsync(dto.Name);
        if (exists)
        {
            throw new ConflictException(ErrorMessages.Genre.NameExists);
        }

        var genre = _mapper.Map<Genre>(dto);
        await _unitOfWork.Genres.AddAsync(genre);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<GenreDto>(genre);
    }

    public async Task DeleteGenreAsync(int genreId)
    {
        var genre = await _unitOfWork.Genres.GetByIdAsync(genreId)
            ?? throw new NotFoundException(ErrorMessages.Genre.NotFound);
        _unitOfWork.Genres.Remove(genre);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<GenreDto>> GetAllGenresAsync()
    {
        var genres = await _unitOfWork.Genres.GetAllAsync();
        return genres.Select(g => _mapper.Map<GenreDto>(g));
    }

    public async Task<GenreDto> GetGenreByIdAsync(int genreId)
    {
        var genre = await _unitOfWork.Genres.GetByIdAsync(genreId)
            ?? throw new NotFoundException(ErrorMessages.Genre.NotFound);

        return _mapper.Map<GenreDto>(genre);
    }

    public async Task<GenreDto> UpdateGenreAsync(int genreId, UpdateGenreDto dto)
    {
        var genre = await _unitOfWork.Genres.GetByIdAsync(genreId)
            ?? throw new NotFoundException(ErrorMessages.Genre.NotFound);

        _mapper.Map(dto, genre);
        _unitOfWork.Genres.Update(genre);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<GenreDto>(genre);
    }
}
