using GoKinoGo.Entities;

namespace GoKinoGo.DTOs.MovieCollection;

public record UpdateMovieCollectionDto
{
    public string? Name { get; init; }
    public CollectionType? Type { get; init; }
    public bool? IsActive { get; init; }
}
