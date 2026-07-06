namespace GoKinoGo.Entities;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public int OwnerId { get; set; }
    public User Owner { get; set; } = null!;
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;
    public ICollection<Like> Likes { get; set; } = [];
}
