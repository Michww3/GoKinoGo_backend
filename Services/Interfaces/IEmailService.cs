namespace GoKinoGo.Services.Interfaces;

public interface IEmailService
{
    Task SendEmailVerificationAsync(string email, string userName, string verificationUrl);
}
