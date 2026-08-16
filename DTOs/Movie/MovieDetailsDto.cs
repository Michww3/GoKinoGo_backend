using GoKinoGo.DTOs.Genre;

namespace GoKinoGo.DTOs.Movie;

public record MovieDetailsDto
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required decimal Price { get; init; }
    public required DateTime ReleaseDate { get; init; }
    public required TimeSpan Length { get; init; }
    public required string PosterUrl { get; init; }
    public required List<GenreDto> Genres { get; init; }

    public double AverageRating { get; init; }
    public int RatingsCount { get; init; }
    public int? UserRating { get; init; }
}
