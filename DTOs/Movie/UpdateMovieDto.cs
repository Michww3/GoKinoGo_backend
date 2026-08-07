using System.ComponentModel.DataAnnotations;

namespace GoKinoGo.DTOs.Movie;

public record UpdateMovieDto
{
    [StringLength(150, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 150 characters.")]
    public string? Name { get; init; }
    [StringLength(5000, MinimumLength = 1, ErrorMessage = "Description must be between 1 and 5000 characters.")]
    public string? Description { get; init; }
    [Range(0, 1000000, ErrorMessage = "Price must be a positive value.")]
    public decimal? Price { get; init; }
    [DataType(DataType.Date)]
    public DateTime? ReleaseDate { get; init; }
    [DataType(DataType.Time)]
    public TimeSpan? Length { get; init; }
    [Url]
    public string? PosterUrl { get; init; }
    public List<int>? GenreIds { get; init; }
}
