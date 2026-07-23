using GoKinoGo.DTOs.User;

namespace GoKinoGo.DTOs.Comment;

public record CommentDto
{
    public required int Id { get; init; }
    public required string Content { get; init; }
    public required DateTime CreationDate { get; init; }
    public required UserDto Owner { get; init; }
    public required int LikesCount { get; init; }
    public required bool IsLikedByCurrentUser { get; set;}
}
