using AutoMapper;
using GoKinoGo.DTOs.Comment;
using GoKinoGo.Entities;

namespace GoKinoGo.Mapping;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CommentDto>()
            .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src => src.Likes.Count))
            .ForMember(dest => dest.IsLikedByCurrentUser, opt => opt.Ignore());
    }
}
