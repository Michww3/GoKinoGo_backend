using GoKinoGo.Entities;

namespace GoKinoGo.DataAccess.Repositories.Interfaces;

public interface ILikeRepository : IRepository<Like>
{
    Task<HashSet<int>> GetLikedCommentIdsAsync(int userId, IEnumerable<int> commentIds);
    Task<Like?> GetByCommentAndUserAsync(int commentId, int userId);
}
