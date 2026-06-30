namespace GoKinoGo.DTOs.Comment;

public class CreateCommentDto
{
    public string Content { get; set; } = string.Empty;
    public int MovieId { get; set; }
}
