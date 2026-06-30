using System.ComponentModel.DataAnnotations;

namespace GoKinoGo.DTOs.Genre;

public class CreateGenreDto
{
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;
}
