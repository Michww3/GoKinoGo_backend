using AutoMapper;
using GoKinoGo.DataAccess.UnitOfWork;
using GoKinoGo.DTOs.Comment;
using GoKinoGo.DTOs.User;
using GoKinoGo.Entities;
using GoKinoGo.Exceptions;
using GoKinoGo.Services.Interfaces;

namespace GoKinoGo.Services;

public class CommentService(IUnitOfWork unitOfWork, IMapper mapper) : ICommentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<CommentDto> CreateCommentAsync(CreateCommentDto dto, int userId)
    {
        _ = await _unitOfWork.Movies.GetByIdAsync(dto.MovieId)
            ?? throw new NotFoundException("Movie not found.");

        var comment = _mapper.Map<Comment>(dto);
        comment.OwnerId = userId;
        comment.CreationDate = DateTime.UtcNow;

        await _unitOfWork.Comments.AddAsync(comment);
        await _unitOfWork.SaveChangesAsync();

        var createdComment = await _unitOfWork.Comments.GetWithDetailsByIdAsync(comment.Id)
            ?? throw new InvalidOperationException("Created comment cannot be loaded.");

        return _mapper.Map<CommentDto>(createdComment);
    }

    public async Task DeleteCommentAsync(int commentId, CurrentUserDto currentUser)
    {
        var comment = await _unitOfWork.Comments.GetByIdAsync(commentId)
            ?? throw new NotFoundException("Comment not found.");

        if (comment.OwnerId != currentUser.Id && currentUser.Role != UserRole.Admin)
            throw new ForbiddenException("User does not have permission to delete this comment.");

        _unitOfWork.Comments.Remove(comment);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<CommentDto> GetCommentByIdAsync(int id, int? currentUserId = null)
    {
        var comment = await _unitOfWork.Comments.GetWithDetailsByIdAsync(id)
            ?? throw new NotFoundException("Comment does not exist.");

        var commentDto = _mapper.Map<CommentDto>(comment);
        if (currentUserId.HasValue)
        {
            commentDto.IsLikedByCurrentUser = await _unitOfWork.Comments.IsLikedByUserAsync(comment.Id, currentUserId.Value);
        }
        return commentDto;
    }

    public async Task<IEnumerable<CommentDto>> GetCommentsByMovieAsync(int movieId, int? currentUserId = null)
    {
        var comments = await _unitOfWork.Comments.GetByMovieIdAsync(movieId);
        var commentDtos = _mapper.Map<List<CommentDto>>(comments);

        if (currentUserId.HasValue)
        {
            var likedIds = await _unitOfWork.Likes.GetLikedCommentIdsAsync(
                currentUserId.Value,
                commentDtos.Select(c => c.Id));

            foreach (var dto in commentDtos)
            {
                dto.IsLikedByCurrentUser = likedIds.Contains(dto.Id);
            }
        }
        return commentDtos;
    }

    public async Task<bool> ToggleLikeAsync(int commentId, int userId)
    {
        _ = await _unitOfWork.Comments.GetByIdAsync(commentId)
            ?? throw new NotFoundException("Comment not found.");

        var like = await _unitOfWork.Likes.GetByCommentAndUserAsync(commentId, userId);

        if (like != null)
        {
            _unitOfWork.Likes.Remove(like);
            await _unitOfWork.SaveChangesAsync();
            return false;
        }
        else
        {
            var newLike = new Like
            {
                UserId = userId,
                CommentId = commentId,
                CreatedAt = DateTime.UtcNow,
            };
            await _unitOfWork.Likes.AddAsync(newLike);
        }

        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
