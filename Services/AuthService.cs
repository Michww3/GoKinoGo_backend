using AutoMapper;
using GoKinoGo.DataAccess.UnitOfWork;
using GoKinoGo.DTOs.Auth;
using GoKinoGo.DTOs.User;
using GoKinoGo.Entities;
using GoKinoGo.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GoKinoGo.Services;

public class AuthService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<JwtOptions> jwtOptions) : IAuthService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly JwtOptions _jwt = jwtOptions.Value;

    public async Task<AuthResponseDto> RegisterAsync(CreateUserDto dto)
    {
        var emailExists = await _unitOfWork.Users
            .ExistsByEmailAsync(dto.Email);
        if (emailExists)
            throw new InvalidOperationException("User with this email already exists.");

        var userNameExists = await _unitOfWork.Users
            .ExistsByUserNameAsync(dto.UserName);
        if (userNameExists)
            throw new InvalidOperationException("User with this username already exists.");

        var user = _mapper.Map<User>(dto);

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return new AuthResponseDto
        {
            Token = GenerateJwt(user),
            User = _mapper.Map<UserDto>(user)
        };
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
    {
        var user = await _unitOfWork.Users
            .GetByEmailAsync(dto.Email);

        if (user == null)
            return null;

        var valid = BCrypt.Net.BCrypt.Verify(
            dto.Password,
            user.PasswordHash);

        if (!valid)
            return null;

        return new AuthResponseDto
        {
            Token = GenerateJwt(user),
            User = _mapper.Map<UserDto>(user)
        };
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
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
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
}
