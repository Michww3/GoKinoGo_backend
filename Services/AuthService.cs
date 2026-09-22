using AutoMapper;
using GoKinoGo.Constants;
using GoKinoGo.DataAccess.UnitOfWork;
using GoKinoGo.DTOs.Auth;
using GoKinoGo.DTOs.User;
using GoKinoGo.Entities;
using GoKinoGo.Exceptions;
using GoKinoGo.Options;
using GoKinoGo.Services.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GoKinoGo.Services;

public class AuthService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<JwtOptions> jwtOptions, IOptions<FrontendOptions> frontendOptions, IPasswordHasherService passwordHasher, IEmailService emailService) : IAuthService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly JwtOptions _jwt = jwtOptions.Value;
    private readonly FrontendOptions _frontend = frontendOptions.Value;
    private readonly IPasswordHasherService _passwordHasher = passwordHasher;
    private readonly IEmailService _emailService = emailService;

    public async Task<AuthResponseDto> RegisterAsync(CreateUserDto dto, UserRole userRole = UserRole.User)
    {
        var emailExists = await _unitOfWork.Users
            .ExistsByEmailAsync(dto.Email);
        if (emailExists)
            throw new ConflictException(ErrorMessages.User.EmailExists);

        var userNameExists = await _unitOfWork.Users
            .ExistsByUserNameAsync(dto.UserName);
        if (userNameExists)
            throw new ConflictException(ErrorMessages.User.UserNameExists);

        var user = _mapper.Map<User>(dto);

        user.PasswordHash = _passwordHasher.HashPassword(dto.Password);
        user.Role = userRole;
        user.EmailConfirmed = false;

        await _unitOfWork.Users.AddAsync(user);

        var token = GenerateVerificationToken();
        var verificationToken = new EmailVerificationToken
        {
            TokenHash = HashToken(token),
            ExpiresAt = DateTime.UtcNow.AddMinutes(10),
            User = user
        };

        await _unitOfWork.EmailVerificationTokens.AddAsync(verificationToken);

        await _unitOfWork.SaveChangesAsync();

        var verificationUrl = $"{_frontend.BaseUrl.TrimEnd('/')}/verify-email?token={token}"; 
        await _emailService.SendEmailVerificationAsync(user.Email, user.UserName, verificationUrl);

        return new AuthResponseDto
        {
            Token = GenerateJwt(user),
            User = _mapper.Map<UserDto>(user)
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(dto.Email)
            ?? throw new UnauthorizedException(ErrorMessages.Auth.InvalidCredentials);

        var valid = _passwordHasher.VerifyPassword(dto.Password, user.PasswordHash);
        if (!valid)
            throw new UnauthorizedException(ErrorMessages.Auth.InvalidCredentials);

        return new AuthResponseDto
        {
            Token = GenerateJwt(user),
            User = _mapper.Map<UserDto>(user)
        };
    }

    public async Task<UserDto> Me(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id)
            ?? throw new NotFoundException(ErrorMessages.User.NotFound);
        return _mapper.Map<UserDto>(user);
    }

    public async Task ConfirmEmailAsync(string token)
    {
        var tokenHash = HashToken(token);

        var verificationToken = await _unitOfWork.EmailVerificationTokens
            .GetByTokenHashAsync(tokenHash)
            ?? throw new BadRequestException(ErrorMessages.Auth.InvalidVerificationToken);

        if(verificationToken.ExpiresAt < DateTime.UtcNow)
            throw new BadRequestException(ErrorMessages.Auth.InvalidVerificationToken);

        verificationToken.User.EmailConfirmed = true;
        _unitOfWork.EmailVerificationTokens.Remove(verificationToken);
        await _unitOfWork.SaveChangesAsync();
    }

    private string GenerateJwt(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwt.Key));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwt.ExpireMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
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
}
