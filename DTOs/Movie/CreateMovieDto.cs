using System.ComponentModel.DataAnnotations;

namespace GoKinoGo.DTOs.Movie;

public class CreateMovieDto
{
    [Required]
    [StringLength(150, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public DateTime ReleaseDate { get; set; }

    [Required]
    public TimeSpan Length { get; set; }
    [Url]
    public string PosterUrl { get; set; } = string.Empty;

    public List<int> GenreIds { get; set; } = [];
}
