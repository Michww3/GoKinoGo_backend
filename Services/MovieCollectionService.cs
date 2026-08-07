using AutoMapper;
using GoKinoGo.Constants;
using GoKinoGo.DataAccess.UnitOfWork;
using GoKinoGo.DTOs.Movie;
using GoKinoGo.DTOs.MovieCollection;
using GoKinoGo.Entities;
using GoKinoGo.Exceptions;
using GoKinoGo.Services.Interfaces;

namespace GoKinoGo.Services;

public class MovieCollectionService(IMapper mapper, IUnitOfWork unitOfWork) : IMovieCollectionService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IEnumerable<MovieCollectionDto>> GetAllAsync()
    {
        var collections = await _unitOfWork.MovieCollections.GetAllAsync();
        return collections.Select(c => _mapper.Map<MovieCollectionDto>(c));
    }

    public async Task<MovieCollectionDto> CreateAsync(CreateMovieCollectionDto dto)
    {
        if (await _unitOfWork.MovieCollections.ExistsByNameAsync(dto.Name))
            throw new ConflictException(ErrorMessages.MovieCollection.NameExists);
        
        var collection = _mapper.Map<MovieCollection>(dto);

        await _unitOfWork.MovieCollections.AddAsync(collection);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<MovieCollectionDto>(collection);
    }

    public async Task<MovieCollectionDto> GetByIdAsync(int id)
    {
        var collection = await _unitOfWork.MovieCollections.GetByIdWithMoviesAsync(id)
            ?? throw new NotFoundException(ErrorMessages.MovieCollection.NotFound);

        return _mapper.Map<MovieCollectionDto>(collection);
    }


    public async Task DeleteAsync(int id)
    {
        var collection = await _unitOfWork.MovieCollections.GetByIdAsync(id)
            ?? throw new NotFoundException(ErrorMessages.MovieCollection.NotFound);

        _unitOfWork.MovieCollections.Remove(collection);

        await _unitOfWork.SaveChangesAsync();
    }


    public async Task<IEnumerable<MovieDto>> GetMoviesAsync(int collectionId)
    {
        _ = await _unitOfWork.MovieCollections.GetByIdAsync(collectionId)
            ?? throw new NotFoundException(ErrorMessages.MovieCollection.NotFound);

        var movies = await _unitOfWork.MovieCollections.GetMoviesAsync(collectionId);

        return _mapper.Map<IEnumerable<MovieDto>>(movies);
    }

    public async Task<MovieCollectionDto> UpdateAsync(int id, UpdateMovieCollectionDto dto)
    {
        var collection = await _unitOfWork.MovieCollections.GetByIdAsync(id)
            ?? throw new NotFoundException(ErrorMessages.MovieCollection.NotFound);
        if (dto.Name is not null && dto.Name != collection.Name 
            && await _unitOfWork.MovieCollections.ExistsByNameAsync(dto.Name))
        {
                throw new ConflictException(ErrorMessages.MovieCollection.NameExists);        
        }

        _mapper.Map(dto, collection);
        await _unitOfWork.SaveChangesAsync();

        var result = await _unitOfWork.MovieCollections
            .GetByIdWithMoviesAsync(id);

        return _mapper.Map<MovieCollectionDto>(result);
    }
}
