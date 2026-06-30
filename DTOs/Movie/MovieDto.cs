using GoKinoGo.DTOs.Genre;

namespace GoKinoGo.DTOs.Movie;

public class MovieDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public TimeSpan Length { get; set; }
    public string PosterUrl { get; set; } = string.Empty;
    public List<GenreDto> Genres { get; set; } = [];
}
