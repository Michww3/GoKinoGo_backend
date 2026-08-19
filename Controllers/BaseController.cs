using GoKinoGo.Constants;
using GoKinoGo.DTOs.User;
using GoKinoGo.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GoKinoGo.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    protected CurrentUserDto GetCurrentUser()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException(ErrorMessages.User.Unauthorized);
        
        var userId = int.Parse(userIdClaim.Value);
        var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
        var role = Enum.TryParse<UserRole>(roleClaim, out var parsedRole)
            ? parsedRole
            : UserRole.User;

        return new CurrentUserDto
        {
            Id = userId,
            Role = role
        };
    }

    protected int GetCurrentUserId()
    {
        var currentUser = GetCurrentUser()
            ?? throw new UnauthorizedAccessException(ErrorMessages.User.Unauthorized);
        return currentUser.Id;
    }

    protected int? GetCurrentUserIdOrNull()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);

        return claim == null
            ? null
            : int.Parse(claim.Value);
    }
}
