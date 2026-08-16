namespace GoKinoGo.Entities;

public class MovieRating
{
    public int Id { get; set; }
    public int Value { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
