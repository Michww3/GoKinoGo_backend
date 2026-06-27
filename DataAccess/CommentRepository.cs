using GoKinoGo.Data;
using GoKinoGo.DataAccess.Interfaces;
using GoKinoGo.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoKinoGo.DataAccess;

public class CommentRepository : Repository<Comment>, ICommentRepository
{
    public CommentRepository(AppDbContext context) : base(context) { }
    public async Task<IEnumerable<Comment>> GetByMovieIdAsync(int movieId)
    {
        return await _dbSet
            .Include(c => c.Owner)
            .Include(c => c.LikedByUsers)
            .Where(c => c.MovieId == movieId)
            .OrderByDescending(c => c.CreationDate)
            .ToListAsync();
    }
    public async Task<Comment?> GetWithDetailsByIdAsync(int commentId)
    {
        return await _dbSet
            .Include(c => c.Owner)
            .Include (c => c.Movie)
            .Include(c => c.LikedByUsers)
            .SingleAsync(c => c.Id == commentId);
    }

    public async Task<bool> IsLikedByUserAsync(int commentId, int userId)
    {
        return await _dbSet
            .AnyAsync(c => c.Id == commentId && c.LikedByUsers.Any(u => u.Id == userId));
    }
}
