namespace GoKinoGo.DTOs.CollectionItem;

public record AddCollectionItemDto
{
    public required int MovieId { get; init; }
    public required int Position { get; init; }
}
