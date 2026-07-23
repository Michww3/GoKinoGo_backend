using System.ComponentModel.DataAnnotations;

namespace GoKinoGo.DTOs.Comment;

public record CreateCommentDto
{
    [Required]
    [StringLength(5000, MinimumLength = 1, ErrorMessage = "Content must be between 1 and 5000 characters.")]
    public required string Content { get; init; }
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "MovieId must be a positive integer.")]
    public required int MovieId { get; init; }
}
