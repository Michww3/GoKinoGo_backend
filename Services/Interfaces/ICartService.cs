using GoKinoGo.DTOs.Cart;
using GoKinoGo.DTOs.Movie;

namespace GoKinoGo.Services.Interfaces;

public interface ICartService
{
    Task<(IEnumerable<ValidatedCartItemDto>, CartValidationStatus)> ValidateAsync(IEnumerable<CartItemDto> items);

    public enum CartValidationStatus
    {
        Valid,
        PriceChanged,
        MovieNotFound
    }
}
