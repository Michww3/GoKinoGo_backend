using AutoMapper;
using GoKinoGo.DTOs.Movie;
using GoKinoGo.Entities;

namespace GoKinoGo.Mapping;

public class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<Movie, MovieDto>();
        CreateMap<Movie, MovieCardDto>();
        CreateMap<Movie, MovieDetailsDto>();
        CreateMap<CreateMovieDto, Movie>()
            .ForMember(dest => dest.Genres, opt => opt.Ignore());
        CreateMap<UpdateMovieDto, Movie>()
            .ForMember(x => x.Genres, opt => opt.Ignore())
            .ForMember(x => x.ReleaseDate, opt =>
                opt.PreCondition(src => src.ReleaseDate.HasValue))
            .ForMember(x => x.Length, opt =>
                opt.PreCondition(src => src.Length.HasValue))
            .ForMember(x => x.Price, opt =>
                opt.PreCondition(src => src.Price.HasValue))
            .ForAllMembers(opt =>
                opt.Condition((src, dest, value) => value != null));

    }
}
