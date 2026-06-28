using GoKinoGo.Data;
using GoKinoGo.DataAccess.Repositories.Interfaces;
using GoKinoGo.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoKinoGo.DataAccess.Repositories;

public class CommentRepository(AppDbContext context) : Repository<Comment>(context), ICommentRepository
{
    public async Task<IEnumerable<Comment>> GetByMovieIdAsync(int movieId)
    {
        return await _dbSet
            .Include(c => c.Owner)
            .Include(c => c.Likes)
            .ThenInclude(l => l.User)
            .Where(c => c.MovieId == movieId)
            .OrderByDescending(c => c.CreationDate)
            .ToListAsync();
    }
    public async Task<Comment?> GetWithDetailsByIdAsync(int commentId)
    {
        return await _dbSet
            .Include(c => c.Owner)
            .Include (c => c.Movie)
            .Include(c => c.Likes)
            .ThenInclude(l => l.User)
            .SingleAsync(c => c.Id == commentId);
    }

    public async Task<bool> IsLikedByUserAsync(int commentId, int userId)
    {
        return await _context.Likes
            .AnyAsync(l => l.CommentId == commentId && l.UserId == userId);
    }
}
