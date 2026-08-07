namespace GoKinoGo.Entities;

public class CollectionItem
{
    public int Id { get; set; }
    public int Position { get; set; }
    public int CollectionId { get; set; }
    public MovieCollection Collection { get; set; } = null!;
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;
}
