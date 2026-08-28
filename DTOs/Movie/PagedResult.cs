namespace GoKinoGo.DTOs.Movie;

public record PagedResult<T> where T : class
{
    public IEnumerable<T> Items { get; init; } = [];
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public int TotalPages { get; init; }
}
