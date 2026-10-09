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

    public async Task SendAsync(string email, string subject, string htmlBody)
    {
        var message = new MimeMessage();

        message.From.Add(MailboxAddress.Parse(_options.From));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = subject;

        message.Body = new BodyBuilder
        {
            HtmlBody = htmlBody
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