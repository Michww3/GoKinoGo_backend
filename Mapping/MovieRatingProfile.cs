using AutoMapper;
using GoKinoGo.DTOs.MovieRating;
using GoKinoGo.Entities;

namespace GoKinoGo.Mapping;

public class MovieRatingProfile : Profile
{
    public MovieRatingProfile()
    {
        CreateMap<MovieRating, MovieRatingDto>();
    }
}
