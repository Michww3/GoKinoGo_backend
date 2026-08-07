using AutoMapper;
using GoKinoGo.DTOs.MovieCollection;
using GoKinoGo.Entities;

namespace GoKinoGo.Mapping;

public class MovieCollectionProfile : Profile
{
    public MovieCollectionProfile()
    {
        CreateMap<MovieCollection, MovieCollectionDto>();
        CreateMap<CreateMovieCollectionDto, MovieCollection>();
        CreateMap<UpdateMovieCollectionDto, MovieCollection>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
