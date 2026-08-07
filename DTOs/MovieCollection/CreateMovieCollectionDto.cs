using GoKinoGo.Entities;
using System.ComponentModel.DataAnnotations;

namespace GoKinoGo.DTOs.MovieCollection;

public record CreateMovieCollectionDto
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public required string Name { get; init; }
    [Required]
    public required CollectionType Type { get; init; }
    [Required]
    public required bool IsActive { get; init; }
}
