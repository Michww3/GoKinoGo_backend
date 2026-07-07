using AutoMapper;
using GoKinoGo.Constants;
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

    public async Task<UserDto> GetUserByIdAsync(int userId)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId)
            ?? throw new NotFoundException(ErrorMessages.User.NotFound);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetUserByEmailAsync(string email)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(email)
            ?? throw new NotFoundException(ErrorMessages.User.NotFound);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> UpdateUserAsync(int userId, UpdateUserDto dto, CurrentUserDto currentUser)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId)
            ?? throw new NotFoundException(ErrorMessages.User.NotFound);

        if (user.Id != currentUser.Id && currentUser.Role != UserRole.Admin)
            throw new ForbiddenException(ErrorMessages.User.CannotUpdateOtherUser);

        if (dto.Email != null && dto.Email != user.Email && await _unitOfWork.Users.ExistsByEmailAsync(dto.Email))
            throw new ConflictException(ErrorMessages.User.EmailExists);

        if (dto.UserName != null && dto.UserName != user.UserName && await _unitOfWork.Users.ExistsByUserNameAsync(dto.UserName))
            throw new ConflictException(ErrorMessages.User.UserNameExists);

        _mapper.Map(dto, user);

        if (!string.IsNullOrWhiteSpace(dto.Password))
            user.PasswordHash = _passwordHasher.HashPassword(dto.Password);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<UserDto>(user);
    }

    public async Task DeleteUserAsync(int userId, CurrentUserDto currentUser)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId)
            ?? throw new NotFoundException(ErrorMessages.User.NotFound);
        if (user.Id != currentUser.Id && currentUser.Role != UserRole.Admin)
            throw new ForbiddenException(ErrorMessages.User.CannotDeleteOtherUser);

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
