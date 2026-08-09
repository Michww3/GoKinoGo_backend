using AutoMapper;
using GoKinoGo.DTOs.CollectionItem;
using GoKinoGo.Entities;

namespace GoKinoGo.Mapping;

public class CollectionItemProfile : Profile
{
    public CollectionItemProfile()
    {
        CreateMap<CollectionItem, CollectionItemDto>();
    }
}
