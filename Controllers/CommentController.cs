using GoKinoGo.DTOs.Comment;
using GoKinoGo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoKinoGo.Controllers;

[Route("api/movies/{movieId:int}/[controller]")]
public class CommentController(ICommentService commentService) : BaseController
{
    private readonly ICommentService _commentService = commentService;

    /// <summary>
    /// Get all comments for a movie
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<CommentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetByMovie(int movieId)
    {
        var currentUser = GetCurrentUser();
        var comments = await _commentService.GetCommentsByMovieAsync(
            movieId,
            currentUser?.Id);
        return Ok(comments);
    }

    /// <summary>
    /// Get comment by ID
    /// </summary>
    [HttpGet("{commentId:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CommentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CommentDto>> GetById(int movieId, int commentId)
    {
        var currentUser = GetCurrentUser();
        var comment = await _commentService.GetCommentByIdAsync(
            commentId,
            currentUser?.Id);
        return Ok(comment);
    }

    /// <summary>
    /// Create new comment (authenticated users only)
    /// </summary>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(CommentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CommentDto>> Create(int movieId, [FromBody] CreateCommentDto dto)
    {
        dto.MovieId = movieId;
        var userId = GetCurrentUserId();
        var comment = await _commentService.CreateCommentAsync(dto, userId);
        return CreatedAtAction(nameof(GetById), new { movieId, commentId = comment.Id }, comment);
    }

    /// <summary>
    /// Delete comment (owner or admin only)
    /// </summary>
    [HttpDelete("{commentId:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int movieId, int commentId)
    {
        var currentUser = GetCurrentUser();
        await _commentService.DeleteCommentAsync(commentId, currentUser);
        return NoContent();
    }

    /// <summary>
    /// Toggle like on comment (authenticated users only)
    /// </summary>
    [HttpPost("{commentId:int}/like")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ToggleLike(int movieId, int commentId)
    {
        var userId = GetCurrentUserId();
        var isLiked = await _commentService.ToggleLikeAsync(commentId, userId);
        return Ok(new { isLiked });
    }
}
