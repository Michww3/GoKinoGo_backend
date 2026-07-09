using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace GoKinoGo.DTOs.Genre;

public class UpdateGenreDto
{
    [StringLength(50, MinimumLength = 2)]
    public string? Name { get; set; }
}
