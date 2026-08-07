using GoKinoGo.Entities;

namespace GoKinoGo.DTOs.MovieCollection;

public record MovieCollectionDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required CollectionType Type { get; init; }
    public required bool IsActive { get; init; }
}
