using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace GoKinoGo.DTOs.Genre;

public record UpdateGenreDto
{
    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
    public required string Name { get; set; }
}
