using System.ComponentModel.DataAnnotations;

namespace GoKinoGo.DTOs.Cart;

public record ReceiptRequestDto
{
    [Required]
    [EmailAddress]
    [StringLength(100)]
    public required string Email { get; init; }
    public string? UserName { get; init; }
    [Required]
    public required IEnumerable<CartItemDto> Items { get; init; }
}
