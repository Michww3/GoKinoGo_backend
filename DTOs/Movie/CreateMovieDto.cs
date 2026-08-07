using System.ComponentModel.DataAnnotations;

namespace GoKinoGo.DTOs.Movie;

public record CreateMovieDto
{
    [Required]
    [StringLength(150, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 150 characters.")]
    public required string Name { get; init;}
    [Required]
    [StringLength(5000, MinimumLength = 1, ErrorMessage = "Description must be between 1 and 5000 characters.")]
    public required string Description { get; init;}
    [Required]
    [Range(0, 1000000, ErrorMessage = "Price must be a positive value.")]
    public required decimal Price { get; init; }
    [Required]
    [DataType(DataType.Date)]
    public required DateTime ReleaseDate { get; init; }
    [Required]
    [DataType(DataType.Time)]
    public required TimeSpan Length { get; init; }
    [Url]
    public string PosterUrl { get; init;} = string.Empty;
    [Required]
    public required List<int> GenreIds { get; init;}
}
