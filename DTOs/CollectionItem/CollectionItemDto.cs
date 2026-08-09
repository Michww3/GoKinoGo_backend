using GoKinoGo.DTOs.Movie;

namespace GoKinoGo.DTOs.CollectionItem;

public record CollectionItemDto
{
    public required int Position { get; init; }
    public required MovieDto Movie { get; init; }
}
