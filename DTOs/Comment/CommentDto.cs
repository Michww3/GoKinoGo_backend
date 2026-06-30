using GoKinoGo.DTOs.User;

namespace GoKinoGo.DTOs.Comment;

public class CommentDto
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public UserDto Owner { get; set; } = null!;
    public int LikesCount { get; set; }
    public bool isLikedByCurrentUser { get; set; }
}
