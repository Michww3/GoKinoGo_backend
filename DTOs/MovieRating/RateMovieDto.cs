using GoKinoGo.Constants;
using System.ComponentModel.DataAnnotations;

namespace GoKinoGo.DTOs.MovieRating;

public record RateMovieDto
{
    [Required]
    [Range(0, 10,ErrorMessage = ErrorMessages.MovieRating.InvalidValue)]
    public required int Value { get; init; }
}
