using AutoMapper;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<GeoPointEntity, GeoPointDto>().ReverseMap();
        CreateMap<PolygonEntity, PolygonDto>().ReverseMap();
        CreateMap<CircleEntity, CircleDto>().ReverseMap();
    }
}
