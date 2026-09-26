using GoKinoGo.Constants;
using GoKinoGo.DTOs.Auth;
using GoKinoGo.DTOs.User;
using GoKinoGo.Entities;
using GoKinoGo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoKinoGo.Controllers;

[Route("api/[controller]")]
public class AuthController(IAuthService authService) : BaseController
{
    private readonly IAuthService _authService = authService;

    /// <summary>
    /// Register a new user
    /// </summary>
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<AuthResponseDto>> Register(CreateUserDto dto)
    {
        var result = await _authService.RegisterAsync(dto);
        return CreatedAtAction(nameof(Register), result);
    }
    /// <summary>
    /// Register a new admin user
    /// </summary>
    [HttpPost("register-admin")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<AuthResponseDto>> RegisterAdmin(CreateUserDto dto)
    {
        var result = await _authService.RegisterAsync(dto, UserRole.Admin);
        return CreatedAtAction(nameof(Register), result);
    }

    /// <summary>
    /// Login user and get JWT token
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);
        return Ok(result);
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto dto)
    {
        await _authService.ConfirmEmailAsync(dto.Token);

        return Ok(new
        {
            message = Messages.EmailConfirmed
        });
    }

    [Authorize]
    [HttpPost("resend-confirmation")]
    public async Task<IActionResult> ResendConfirmation()
    {
        var userId = GetCurrentUserId();

        await _authService.ResendConfirmationEmailAsync(userId);

        return Ok(new
        {
            message = Messages.EmailConfirmationResent
        });
    }

    /// <summary>
    /// Get current authorized user
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> Me()
    {
        var userId = GetCurrentUserId();
        var user = await _authService.Me(userId);

        return Ok(user);
    }

}
