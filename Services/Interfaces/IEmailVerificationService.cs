using GoKinoGo.Entities;

namespace GoKinoGo.Services.Interfaces;

public interface IEmailVerificationService
{
    Task SendVerificationEmailAsync(User user);
    Task ConfirmEmailAsync(string token);
}
