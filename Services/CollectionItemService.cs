using GoKinoGo.Constants;
using GoKinoGo.DataAccess.UnitOfWork;
using GoKinoGo.DTOs.CollectionItem;
using GoKinoGo.Entities;
using GoKinoGo.Exceptions;
using GoKinoGo.Services.Interfaces;

namespace GoKinoGo.Services;

public class CollectionItemService(IUnitOfWork unitOfWork) : ICollectionItemService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task AddMovieAsync(int collectionId, AddCollectionItemDto dto)
    {
        _ = await _unitOfWork.MovieCollections.GetByIdAsync(collectionId)
            ?? throw new NotFoundException(ErrorMessages.MovieCollection.NotFound);

        _ = await _unitOfWork.Movies.GetByIdAsync(dto.MovieId)
            ?? throw new NotFoundException(ErrorMessages.Movie.NotFound);

        if (await _unitOfWork.CollectionItems.ExistsAsync(collectionId, dto.MovieId))
            throw new ConflictException(ErrorMessages.CollectionItem.ItemAlreadyInCollection);

        if (await _unitOfWork.CollectionItems.PositionExistsAsync(collectionId, dto.Position))
            throw new ConflictException(ErrorMessages.CollectionItem.PositionExists);

        var item = new CollectionItem
        {
            CollectionId = collectionId,
            MovieId = dto.MovieId,
            Position = dto.Position
        };

        await _unitOfWork.CollectionItems.AddAsync(item);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task RemoveMovieAsync(int collectionId, int movieId)
    {
        var item = await _unitOfWork.CollectionItems.GetByMovieAndCollectionAsync(movieId, collectionId)
            ?? throw new NotFoundException(ErrorMessages.CollectionItem.NotFound);

        _unitOfWork.CollectionItems.Remove(item);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdatePositionAsync(int collectionId, int movieId, UpdateCollectionItemPositionDto dto)
    {
        var item = await _unitOfWork.CollectionItems.GetByMovieAndCollectionAsync(movieId, collectionId)
            ?? throw new NotFoundException(ErrorMessages.CollectionItem.NotFound);

        if (await _unitOfWork.CollectionItems.PositionExistsAsync(collectionId, dto.Position, item.Id))
            throw new ConflictException(ErrorMessages.CollectionItem.PositionExists);

        item.Position = dto.Position;

        await _unitOfWork.SaveChangesAsync();
    }
}
