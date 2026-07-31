using AutoMapper;
using GoKinoGo.Constants;
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

    public async Task<CommentDto> CreateCommentAsync(int movieId, CreateCommentDto dto, int userId)
    {
        _ = await _unitOfWork.Movies.GetByIdAsync(movieId)
            ?? throw new NotFoundException(ErrorMessages.Movie.NotFound);

        var comment = _mapper.Map<Comment>(dto);
        comment.OwnerId = userId;
        comment.CreationDate = DateTime.UtcNow;
        comment.MovieId = movieId;

        await _unitOfWork.Comments.AddAsync(comment);
        await _unitOfWork.SaveChangesAsync();

        var createdComment = await _unitOfWork.Comments.GetWithDetailsByIdAsync(comment.Id)
            ?? throw new InvalidOperationException(ErrorMessages.Comment.CannotLoadCreated);

        return _mapper.Map<CommentDto>(createdComment);
    }

    public async Task DeleteCommentAsync(int commentId, CurrentUserDto currentUser)
    {
        var comment = await _unitOfWork.Comments.GetByIdAsync(commentId)
            ?? throw new NotFoundException(ErrorMessages.Comment.NotFound);

        if (comment.OwnerId != currentUser.Id && currentUser.Role != UserRole.Admin)
            throw new ForbiddenException(ErrorMessages.Comment.CannotDeleteOtherComment);

        _unitOfWork.Comments.Remove(comment);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<CommentDto> GetCommentByIdAsync(int id, int? currentUserId = null)
    {
        var comment = await _unitOfWork.Comments.GetWithDetailsByIdAsync(id)
            ?? throw new NotFoundException(ErrorMessages.Comment.NotFound);

        var commentDto = _mapper.Map<CommentDto>(comment);

        return commentDto with { IsLikedByCurrentUser = currentUserId.HasValue && comment.Likes.Any(like => like.UserId == currentUserId.Value) };
    }

    public async Task<IEnumerable<CommentDto>> GetCommentsByMovieAsync(int movieId, int? currentUserId = null)
    {
        var comments = await _unitOfWork.Comments.GetByMovieIdAsync(movieId);
        var commentDtos = _mapper.Map<List<CommentDto>>(comments);

        if (!currentUserId.HasValue)
            return commentDtos;

        var likedIds = await _unitOfWork.Likes.GetLikedCommentIdsAsync(
            currentUserId.Value,
            commentDtos.Select(c => c.Id));
        var likedIdsSet = likedIds.ToHashSet();

        return commentDtos.Select(dto => dto with { IsLikedByCurrentUser = likedIdsSet.Contains(dto.Id) });
    }

    public async Task<bool> ToggleLikeAsync(int commentId, int userId)
    {
        _ = await _unitOfWork.Comments.GetByIdAsync(commentId)
            ?? throw new NotFoundException(ErrorMessages.Comment.NotFound);

        var like = await _unitOfWork.Likes.GetByCommentAndUserAsync(commentId, userId);

        if (like != null)
        {
            _unitOfWork.Likes.Remove(like);
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
        return like == null;
    }
}
