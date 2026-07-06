using AutoMapper;
using GoKinoGo.DTOs.Movie;
using GoKinoGo.Entities;

namespace GoKinoGo.Mapping;

public class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<Movie, MovieDto>();
        CreateMap<CreateMovieDto, Movie>()
            .ForMember(dest => dest.Genres, opt => opt.Ignore());
        CreateMap<UpdateMovieDto, Movie>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
