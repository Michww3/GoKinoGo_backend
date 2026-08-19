using GoKinoGo.DTOs.MovieRating;
using GoKinoGo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GoKinoGo.Controllers;

[Route("api/movies/{movieId:int}/rating")]
public class MovieRatingController(IMovieRatingService movieRatingService) : BaseController
{
    private readonly IMovieRatingService _movieRatingService = movieRatingService;

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Rate(int movieId, [FromBody] RateMovieDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await _movieRatingService.RateAsync(
            movieId,
            userId,
            dto.Value);

        return NoContent();
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> Delete(int movieId)
    {
        var currentUser = GetCurrentUser();

        await _movieRatingService.DeleteRatingAsync(movieId, currentUser);

        return NoContent();
    }
}
