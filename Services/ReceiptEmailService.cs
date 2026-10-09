using GoKinoGo.DTOs.Cart;
using GoKinoGo.Services.Interfaces;

namespace GoKinoGo.Services;

public class ReceiptEmailService(IEmailService emailService) : IReceiptEmailService
{
    private readonly IEmailService _emailService = emailService;
    public async Task SendReceiptAsync(string email, string? userName, IEnumerable<ValidatedCartItemDto> items)
    {
        userName ??= "Пользователь";
        var cartItems = items.ToList();

        var total = cartItems.Sum(x => x.Price * x.Quantity);

        var itemsHtml = string.Join(
            "",
            cartItems.Select(item => $"""
                <tr>
                    <td>{item.Name}</td>
                    <td>{item.Quantity}</td>
                    <td>{item.Price:F2} BYN</td>
                    <td>{item.Price * item.Quantity:F2} BYN</td>
                </tr>
                """));

        var htmlBody = $"""
            <h2>Здравствуйте, {userName}!</h2>

            <p>Спасибо за покупку.</p>

            <h3>Ваш заказ</h3>

            <table>
                <thead>
                    <tr>
                        <th>Фильм</th>
                        <th>Количество</th>
                        <th>Цена</th>
                        <th>Сумма</th>
                    </tr>
                </thead>

                <tbody>
                    {itemsHtml}
                </tbody>
            </table>

            <p>
                <strong>Итого: {total:F2} BYN</strong>
            </p>
            """;

        await _emailService.SendAsync(email, "Чек за покупку фильмов", htmlBody);
    }
}

