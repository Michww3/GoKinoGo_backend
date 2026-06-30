using System.ComponentModel.DataAnnotations;

namespace GoKinoGo.DTOs.Movie;

public class UpdateMovieDto
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime ReleaseDate { get; set; }

    public TimeSpan Length { get; set; }

    public string PosterUrl { get; set; } = string.Empty;

    public List<int> GenreIds { get; set; } = [];
}
