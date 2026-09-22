using GoKinoGo.DTOs.MovieRating;
using GoKinoGo.DTOs.User;
using GoKinoGo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GoKinoGo.Controllers;

[Route("api/[controller]")]
[Authorize]
public class UsersController(IUserService userService) : BaseController
{
    private readonly IUserService _userService = userService;

    /// <summary>
    /// Get user by ID
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> GetById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return Ok(user);
    }

    /// <summary>
    /// Update user data (only owner or admin)
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UserDto>> Update(int id, [FromBody] UpdateUserDto dto)
    {
        var currentUser = GetCurrentUser();
        var user = await _userService.UpdateUserAsync(id, dto, currentUser);
        return Ok(user);
    }

    /// <summary>
    /// Update user password (only owner)
    /// </summary>
    [HttpPut("{id:int}/password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePassword(int id,[FromBody] UpdateUserPasswordDto dto)
    {
        var currentUser = GetCurrentUser();

        await _userService.UpdatePasswordAsync(id, dto, currentUser);

        return NoContent();
    }

    /// <summary>
    /// Delete user (only owner or admin)
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var currentUser = GetCurrentUser();
        await _userService.DeleteUserAsync(id, currentUser);
        return NoContent();
    }

    /// <summary>
    /// Check if email exists
    /// </summary>
    [HttpGet("check-email")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> CheckEmailExists([FromQuery] string email)
    {
        var exists = await _userService.ExistsByEmailAsync(email);
        return Ok(exists);
    }

    /// <summary>
    /// Check if username exists
    /// </summary>
    [HttpGet("check-username")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> CheckUsernameExists([FromQuery] string userName)
    {
        var exists = await _userService.ExistsByUserNameAsync(userName);
        return Ok(exists);
    }

    [HttpGet("ratings")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<MovieRatingDto>>> GetMyRatings()
    {
        var userId = GetCurrentUserId();

        var ratings = await _userService.GetUserRatings(userId);

        return Ok(ratings);
    }
}
