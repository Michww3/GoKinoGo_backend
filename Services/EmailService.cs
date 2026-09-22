using GoKinoGo.Options;
using GoKinoGo.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace GoKinoGo.Services;

public class EmailService(IOptions<EmailOptions> options) : IEmailService
{
    private readonly EmailOptions _options = options.Value;

    public async Task SendEmailVerificationAsync(string email, string userName, string verificationUrl)
    {
        var message = new MimeMessage();

        message.From.Add(MailboxAddress.Parse(_options.From));

        message.To.Add(MailboxAddress.Parse(email));

        message.Subject = "Подтверждение email";

        message.Body = new BodyBuilder
        {
            HtmlBody = $"""
                <h2>Здравствуйте, {userName}!</h2>

                <p>
                    Для подтверждения email перейдите по ссылке:
                </p>

                <p>
                    <a href="{verificationUrl}">
                        Подтвердить email
                    </a>
                </p>

                <p>
                    Ссылка действительна 10 минут.
                </p>
                """
        }.ToMessageBody();

        using var smtp = new SmtpClient();

        await smtp.ConnectAsync(
            _options.Host,
            _options.Port,
            SecureSocketOptions.StartTls);

        await smtp.AuthenticateAsync(
            _options.Username,
            _options.Password);

        await smtp.SendAsync(message);

        await smtp.DisconnectAsync(true);
    }
}