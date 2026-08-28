using GoKinoGo.DTOs.Movie;
using GoKinoGo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoKinoGo.Controllers;

[Route("api/[controller]")]
public class MoviesController(IMovieService movieService) : BaseController
{
    private readonly IMovieService _movieService = movieService;

    /// <summary>
    /// Get all movies
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<MovieCardDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MovieCardDto>>> GetAll()
    {
        var movies = await _movieService.GetAllMoviesAsync();
        return Ok(movies);
    }

    /// <summary>
    /// Get movies with pagination
    /// </summary>
    [HttpGet("paged")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<MovieCardDto>>> GetPaged([FromQuery] MoviesQuery query)
    {
        var (items, totalCount) =
            await _movieService.GetPagedMoviesAsync(query);

        var totalPages = (int)Math.Ceiling(
            (double)totalCount / query.PageSize);

        return Ok(new PagedResult<MovieCardDto>
        {
            Items = items,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        });
    }

    /// <summary>
    /// Get movie by ID
    /// </summary>
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(MovieDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MovieDetailsDto>> GetById(int id)
    {
        int? userId = GetCurrentUserIdOrNull();
        var movie = await _movieService.GetMovieDetailsByIdAsync(id, userId);
        return Ok(movie);
    }

    /// <summary>
    /// Create new movie (admin only)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(MovieDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<MovieDto>> Create([FromBody] CreateMovieDto dto)
    {
        var movie = await _movieService.CreateMovieAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie);
    }

    /// <summary>
    /// Update movie (admin only)
    /// </summary>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MovieDto>> Update(int id, [FromBody] UpdateMovieDto dto)
    {
        var movie = await _movieService.UpdateMovieAsync(id, dto);
        return Ok(movie);
    }

    /// <summary>
    /// Delete movie (admin only)
    /// </summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await _movieService.DeleteMovieAsync(id);
        return NoContent();
    }
}
