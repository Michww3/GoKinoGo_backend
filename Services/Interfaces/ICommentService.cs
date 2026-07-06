using GoKinoGo.DTOs.Comment;

namespace GoKinoGo.Services.Interfaces;

public interface ICommentService
{
    Task<IEnumerable<CommentDto>> GetCommentsByMovieAsync(int movieId, int? currentUserId = null);
    Task<CommentDto> GetCommentByIdAsync(int id, int? currentUserId = null);
    Task<CommentDto> CreateCommentAsync(CreateCommentDto dto, int userId);
    Task DeleteCommentAsync(int commentId, int userId);
    Task<bool> ToggleLikeAsync(int commentId, int userId);
}
