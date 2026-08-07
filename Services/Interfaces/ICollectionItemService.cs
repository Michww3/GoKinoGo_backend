using GoKinoGo.DTOs.CollectionItem;

namespace GoKinoGo.Services.Interfaces;

public interface ICollectionItemService
{
    Task AddMovieAsync(int collectionId, AddCollectionItemDto dto);
    Task RemoveMovieAsync(int collectionId, int movieId);
    Task UpdatePositionAsync(int collectionId, int movieId, UpdateCollectionItemPositionDto dto);
}
