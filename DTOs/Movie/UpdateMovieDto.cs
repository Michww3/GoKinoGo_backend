using System.ComponentModel.DataAnnotations;

namespace GoKinoGo.DTOs.Movie;

public class UpdateMovieDto
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public TimeSpan? Length { get; set; }

    public string? PosterUrl { get; set; }

    public List<int>? GenreIds { get; set; }
}
