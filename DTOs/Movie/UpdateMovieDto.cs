namespace GoKinoGo.DTOs.Movie;

public record UpdateMovieDto
{
    public string? Name { get; init; }

    public string? Description { get; init; }

    public DateTime? ReleaseDate { get; init; }

    public TimeSpan? Length { get; init; }

    public string? PosterUrl { get; init; }

    public List<int>? GenreIds { get; init; }
}
