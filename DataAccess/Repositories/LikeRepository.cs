using GoKinoGo.Data;
using GoKinoGo.DataAccess.Repositories.Interfaces;
using GoKinoGo.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoKinoGo.DataAccess.Repositories;

public class LikeRepository(AppDbContext context) : Repository<Like>(context), ILikeRepository
{
    public async Task<Like?> GetByCommentAndUserAsync(int commentId, int userId)
    {
        return await _context.Likes
            .SingleOrDefaultAsync(l => l.CommentId == commentId && l.UserId == userId);
    }

    public async Task<HashSet<int>> GetLikedCommentIdsAsync(int userId, IEnumerable<int> commentIds)
    {
        return await _context.Likes
            .Where(l => l.UserId == userId && commentIds.Contains(l.CommentId))
            .Select(l => l.CommentId)
            .ToHashSetAsync();
    }
}
