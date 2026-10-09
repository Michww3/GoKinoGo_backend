namespace GoKinoGo.DTOs.Cart;

public record ValidatedCartItemDto
{
    public required int MovieId { get; init; }
    public required string Name { get; init; }
    public required string PosterUrl { get; init; }
    public required int Quantity { get; init; }
    public required decimal Price { get; init; }
}
