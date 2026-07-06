namespace GoKinoGo.Entities;

public class Like
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int CommentId { get; set; }
    public Comment Comment { get; set; } = null!;
}
