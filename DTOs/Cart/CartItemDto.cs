using System.ComponentModel.DataAnnotations;

namespace GoKinoGo.DTOs.Cart;

public record CartItemDto
{
    public required int MovieId { get; init; }
    [Range(1, 100, ErrorMessage = "Quantity must be greater than 0.")]
    public required int Quantity { get; init; }
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    public required decimal Price { get; init; }
}
