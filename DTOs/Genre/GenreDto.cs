namespace GoKinoGo.DTOs.Genre;

public record GenreDto
{
    public int Id { get; init; }
    public required string Name { get; init; }
}
