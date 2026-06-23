namespace GoKinoGo.Entities;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public int OwnerId {  get; set; }
    public User Owner { get; set; } = new User();
    public int MovieId {  get; set; }
    public Movie Movie { get; set; } = new Movie();
    public ICollection<User> LikedByUsers { get; set; } = [];
}
