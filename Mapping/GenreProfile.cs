using AutoMapper;
using GoKinoGo.DTOs.Genre;
using GoKinoGo.Entities;

namespace GoKinoGo.Mapping;

public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<Genre, GenreDto>();
        CreateMap<CreateGenreDto, Genre>();
        CreateMap<UpdateGenreDto, Genre>()
               .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
