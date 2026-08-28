using System.ComponentModel.DataAnnotations;

namespace GoKinoGo.DTOs.Movie;

public record MoviesQuery
{
    [Range(0, int.MaxValue)]
    public int PageNumber { get; init; } = 1;
    [AllowedValues(12,24,48)]
    public int PageSize { get; init; } = 12;
    public string? SearchQuery { get; init; }
    public int[]? GenreIds { get; init; }
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
    public int? MinYear { get; init; }
    public int? MaxYear { get; init; }
    public double? MinRating { get; init; }
    public string? SortBy { get; init; }
}
