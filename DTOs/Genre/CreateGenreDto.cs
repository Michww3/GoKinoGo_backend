using System.ComponentModel.DataAnnotations;

namespace GoKinoGo.DTOs.Genre;

public record CreateGenreDto
{
    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
    public required string Name { get; init;}
}
