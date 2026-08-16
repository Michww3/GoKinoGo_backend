using GoKinoGo.DTOs.Movie;

namespace GoKinoGo.DTOs.MovieRating;

public record MovieRatingDto
{
    public int Id { get; init; }
    public int Value { get; init; }
    public MovieCardDto Movie { get; init; } = null!;
}
