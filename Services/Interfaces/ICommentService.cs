using GoKinoGo.DTOs.Comment;
using GoKinoGo.DTOs.User;

namespace GoKinoGo.Services.Interfaces;

public interface ICommentService
{
    Task<IEnumerable<CommentDto>> GetCommentsByMovieAsync(int movieId, int? currentUserId = null);
    Task<CommentDto> GetCommentByIdAsync(int id, int? currentUserId = null);
    Task<CommentDto> CreateCommentAsync(int movieId, CreateCommentDto dto, int userId);
    Task DeleteCommentAsync(int commentId, CurrentUserDto currentUser);
    Task<bool> ToggleLikeAsync(int commentId, int userId);
}
