using GoKinoGo.DTOs.Cart;

namespace GoKinoGo.Services.Interfaces;

public interface IReceiptEmailService
{
    public Task SendReceiptAsync(string email, string userName, IEnumerable<ValidatedCartItemDto> items);
}
