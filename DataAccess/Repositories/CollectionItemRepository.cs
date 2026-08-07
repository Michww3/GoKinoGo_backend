using GoKinoGo.Data;
using GoKinoGo.DataAccess.Repositories.Interfaces;
using GoKinoGo.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoKinoGo.DataAccess.Repositories;

public class CollectionItemRepository(AppDbContext context) : Repository<CollectionItem>(context), ICollectionItemRepository
{
    public async Task<CollectionItem?> GetByMovieAndCollectionAsync(int movieId, int collectionId)
    {
        return await _dbSet
            .SingleOrDefaultAsync(ci => ci.MovieId == movieId && ci.CollectionId == collectionId);
    }

    public async Task<bool> ExistsAsync(int collectionId, int movieId)
    {
        return await _dbSet.AnyAsync(ci => ci.CollectionId == collectionId && ci.MovieId == movieId);
    }

    public async Task<bool> PositionExistsAsync(int collectionId, int position, int? excludeItemId = null)
    {
        return await _dbSet
            .AnyAsync(x => x.CollectionId == collectionId && x.Position == position && (excludeItemId == null || x.Id != excludeItemId));
    }
}
