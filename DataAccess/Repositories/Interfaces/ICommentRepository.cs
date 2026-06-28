using GoKinoGo.Entities;

namespace GoKinoGo.DataAccess.Repositories.Interfaces;

public interface ICommentRepository : IRepository<Comment>
{
    Task<IEnumerable<Comment>> GetByMovieIdAsync(int movieId);
    Task<Comment?> GetWithDetailsByIdAsync(int commentId);
    Task<bool> IsLikedByUserAsync(int commentId, int userId);
}
