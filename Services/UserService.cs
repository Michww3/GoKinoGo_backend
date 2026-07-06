using AutoMapper;
using GoKinoGo.DataAccess.UnitOfWork;
using GoKinoGo.DTOs.User;
using GoKinoGo.Services.Interfaces;

namespace GoKinoGo.Services;

public class UserService(IUnitOfWork unitOfWork, IMapper mapper) : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

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

    //TODO: Implement UpdateUserAsync, DeleteUserAsync
    public async Task<UserDto?> UpdateUserAsync(int userId, UpdateUserDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        throw new NotImplementedException();
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
