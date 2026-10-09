using GoKinoGo.Constants;
using GoKinoGo.DTOs.Cart;
using GoKinoGo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoKinoGo.Controllers;

[Route("api/cart")]
public class CartController(IReceiptEmailService receiptEmailService, ICartService cartService) : BaseController
{
    private readonly IReceiptEmailService _emailService = receiptEmailService;
    private readonly ICartService _cartService = cartService;

    /// <summary>
    /// Send receipt to email
    /// </summary>
    [HttpPost("receipt")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> SendReceiptEmail([FromBody] ReceiptRequestDto dto)
    {
        var (result, status) = await _cartService.ValidateAsync(dto.Items);

        switch (status)
        {
            case ICartService.CartValidationStatus.Valid:
                await _emailService.SendReceiptAsync(dto.Email, dto.UserName, result);
                return NoContent();
            case ICartService.CartValidationStatus.PriceChanged:
                return Conflict(new { message = ErrorMessages.Cart.PriceChanged, items = result });
            case ICartService.CartValidationStatus.MovieNotFound:
                return NotFound(new { message = ErrorMessages.Cart.MovieNotFound, items = result });
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
