namespace GoKinoGo.Entities;

public class Movie
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public TimeSpan Length { get; set; }
    public decimal Price { get; set; }
    public string PosterUrl { get; set; } = string.Empty;
    public ICollection<MovieRating> MovieRatings { get; set; } = [];
    public ICollection<Genre> Genres { get; set; } = [];
    public ICollection<Comment> Comments { get; set; } = [];
    public ICollection<CollectionItem> CollectionItems { get; set; } = [];
}
