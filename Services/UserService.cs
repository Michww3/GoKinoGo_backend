using AutoMapper;
using GoKinoGo.DataAccess.UnitOfWork;
using GoKinoGo.DTOs.User;
using GoKinoGo.Entities;
using GoKinoGo.Exceptions;
using GoKinoGo.Services.Interfaces;

namespace GoKinoGo.Services;

public class UserService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher) : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<UserDto?> GetUserByIdAsync(int userId)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        return user == null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(email);
        return user == null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> UpdateUserAsync(int userId, UpdateUserDto dto, CurrentUserDto currentUser)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId)
            ?? throw new NotFoundException("User not found.");

        if (user.Id != currentUser.Id && currentUser.Role != UserRole.Admin)
            throw new ForbiddenException("User does not have permission to update this user.");

        if (dto.Email != null && dto.Email != user.Email && await _unitOfWork.Users.ExistsByEmailAsync(dto.Email))
            throw new ConflictException("Email is already in use.");

        if (dto.UserName != null && dto.UserName != user.UserName && await _unitOfWork.Users.ExistsByUserNameAsync(dto.UserName))
            throw new ConflictException("Username is already in use.");

        _mapper.Map(dto, user);

        if (!string.IsNullOrWhiteSpace(dto.Password))
            user.PasswordHash = _passwordHasher.HashPassword(dto.Password);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<UserDto>(user);
    }

    public async Task DeleteUserAsync(int userId, CurrentUserDto currentUser)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId)
            ?? throw new NotFoundException("User not found.");
        if (user.Id != currentUser.Id && currentUser.Role != UserRole.Admin)
            throw new ForbiddenException("User does not have permission to delete this user.");

        _unitOfWork.Users.Remove(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _unitOfWork.Users.ExistsByEmailAsync(email);
    }

    public async Task<bool> ExistsByUserNameAsync(string userName)
    {
        return await _unitOfWork.Users.ExistsByUserNameAsync(userName);
    }

}
