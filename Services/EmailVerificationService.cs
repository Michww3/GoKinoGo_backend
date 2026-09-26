using GoKinoGo.Constants;
using GoKinoGo.DataAccess.UnitOfWork;
using GoKinoGo.Entities;
using GoKinoGo.Exceptions;
using GoKinoGo.Options;
using GoKinoGo.Services.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace GoKinoGo.Services;

public class EmailVerificationService(IUnitOfWork unitOfWork, IEmailService emailService, IOptions<FrontendOptions> options) : IEmailVerificationService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEmailService _emailService = emailService;
    private readonly FrontendOptions _frontend = options.Value;

    public async Task ConfirmEmailAsync(string token)
    {
        var tokenHash = HashToken(token);

        var verificationToken = await _unitOfWork.EmailVerificationTokens.GetByTokenHashAsync(tokenHash)
            ?? throw new BadRequestException(ErrorMessages.Auth.InvalidVerificationToken);

        if (verificationToken.ExpiresAt < DateTime.UtcNow)
        {
            _unitOfWork.EmailVerificationTokens.Remove(verificationToken);
            await _unitOfWork.SaveChangesAsync();

            throw new BadRequestException(ErrorMessages.Auth.InvalidVerificationToken);
        }

        verificationToken.User.EmailConfirmed = true;
        _unitOfWork.EmailVerificationTokens.Remove(verificationToken);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task SendVerificationEmailAsync(User user)
    {
        var tokens = await _unitOfWork.EmailVerificationTokens.GetByUserIdAsync(user.Id);

        foreach (var token in tokens)
        {
            _unitOfWork.EmailVerificationTokens.Remove(token);
        }

        var tokenValue = await CreateVerificationTokenAsync(user);

        await _unitOfWork.SaveChangesAsync();

        var verificationUrl = $"{_frontend.BaseUrl.TrimEnd('/')}/verify-email?token={tokenValue}";

        await _emailService.SendEmailVerificationAsync(
            user.Email,
            user.UserName,
            verificationUrl);
    }

    private static string GenerateVerificationToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(32);

        return WebEncoders.Base64UrlEncode(bytes);
    }

    private static string HashToken(string token)
    {
        var hash = SHA256.HashData(
            Encoding.UTF8.GetBytes(token));

        return Convert.ToHexString(hash);
    }

    private async Task<string> CreateVerificationTokenAsync(User user)
    {
        var token = GenerateVerificationToken();

        var verificationToken = new EmailVerificationToken
        {
            TokenHash = HashToken(token),
            ExpiresAt = DateTime.UtcNow.AddMinutes(10),
            UserId = user.Id
        };

        await _unitOfWork.EmailVerificationTokens
            .AddAsync(verificationToken);

        return token;
    }
}
