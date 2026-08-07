using GoKinoGo.DTOs.Movie;
using GoKinoGo.DTOs.MovieCollection;
using GoKinoGo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoKinoGo.Controllers;

[Route("api/[controller]")]
public class MovieCollectionsController(IMovieCollectionService movieCollectionService) : BaseController
{
    private readonly IMovieCollectionService _movieCollectionService = movieCollectionService;

    /// <summary>
    /// Get all movie collections
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<MovieCollectionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<MovieCollectionDto>>> GetAll()
    {
        var collections = await _movieCollectionService.GetAllAsync();
        return Ok(collections);
    }

    /// <summary>
    /// Get movie collection by ID
    /// </summary>
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(MovieCollectionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MovieCollectionDto>> GetById(int id)
    {
        var collection = await _movieCollectionService.GetByIdAsync(id);

        return Ok(collection);
    }


    /// <summary>
    /// Get movies from collection
    /// </summary>
    [HttpGet("{id:int}/movies")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<MovieDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies(int id)
    {
        var movies = await _movieCollectionService.GetMoviesAsync(id);

        return Ok(movies);
    }


    /// <summary>
    /// Create new movie collection (admin only)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(MovieCollectionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<MovieCollectionDto>> Create(
        [FromBody] CreateMovieCollectionDto dto)
    {
        var collection = await _movieCollectionService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = collection.Id },
            collection);
    }


    /// <summary>
    /// Update movie collection (admin only)
    /// </summary>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(MovieCollectionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<MovieCollectionDto>> Update(
        int id,
        [FromBody] UpdateMovieCollectionDto dto)
    {
        var collection = await _movieCollectionService.UpdateAsync(id, dto);

        return Ok(collection);
    }


    /// <summary>
    /// Delete movie collection (admin only)
    /// </summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await _movieCollectionService.DeleteAsync(id);

        return NoContent();
    }
}
