using System.ComponentModel.DataAnnotations;

namespace GoKinoGo.DTOs.Movie;

public record CreateMovieDto
{
    [Required]
    [StringLength(150, MinimumLength = 1)]
    public required string Name { get; init;}
    [Required]
    [StringLength(5000, MinimumLength = 1)]
    public required string Description { get; init;}
    [Required]
    [DataType(DataType.Date)]
    public required DateTime ReleaseDate { get; init; }
    [Required]
    public required TimeSpan Length { get; init; }
    [Url]
    public string PosterUrl { get; init;} = string.Empty;
    [Required]
    public required List<int> GenreIds { get; init;}
}
