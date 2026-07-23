using GoKinoGo.DTOs.Genre;

namespace GoKinoGo.DTOs.Movie;

public record MovieDto
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required DateTime ReleaseDate { get; init; }
    public required TimeSpan Length { get; init; }
    public required string PosterUrl { get; init; }
    public required List<GenreDto> Genres { get; init;}
}
