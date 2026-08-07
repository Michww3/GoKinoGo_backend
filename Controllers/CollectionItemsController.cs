using GoKinoGo.DTOs.CollectionItem;
using GoKinoGo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoKinoGo.Controllers;

[Route("api/movie-collections/{collectionId:int}/items")]
public class CollectionItemsController(ICollectionItemService collectionItemService) : BaseController
{
    private readonly ICollectionItemService _collectionItemService = collectionItemService;

    /// <summary>
    /// Add movie to collection (admin only)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AddMovie(
        int collectionId,
        [FromBody] AddCollectionItemDto dto)
    {
        await _collectionItemService.AddMovieAsync(
            collectionId,
            dto);

        return NoContent();
    }


    /// <summary>
    /// Remove movie from collection (admin only)
    /// </summary>
    [HttpDelete("{movieId:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveMovie(
        int collectionId,
        int movieId)
    {
        await _collectionItemService.RemoveMovieAsync(
            collectionId,
            movieId);

        return NoContent();
    }


    /// <summary>
    /// Update movie position in collection (admin only)
    /// </summary>
    [HttpPut("{movieId:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdatePosition(
        int collectionId,
        int movieId,
        [FromBody] UpdateCollectionItemPositionDto dto)
    {
        await _collectionItemService.UpdatePositionAsync(
            collectionId,
            movieId,
            dto);

        return NoContent();
    }
}
