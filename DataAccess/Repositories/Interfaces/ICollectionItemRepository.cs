using GoKinoGo.Entities;

namespace GoKinoGo.DataAccess.Repositories.Interfaces;

public interface ICollectionItemRepository : IRepository<CollectionItem>
{
    public Task<CollectionItem?> GetByMovieAndCollectionAsync(int movieId, int collectionId);
    public Task<bool>  ExistsAsync(int collectionId, int movieId);
    public Task<bool> PositionExistsAsync(int collectionId, int position, int? excludeItemId = null);
}
