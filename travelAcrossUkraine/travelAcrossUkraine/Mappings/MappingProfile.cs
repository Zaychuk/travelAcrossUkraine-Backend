using AutoMapper;
using travelAcrossUkraine.WebApi.Dtos;
using travelAcrossUkraine.WebApi.Entities;

namespace travelAcrossUkraine.WebApi.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<GeoPointEntity, GeoPointDto>().ReverseMap();
    }
}
