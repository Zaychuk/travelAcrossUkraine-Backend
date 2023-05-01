using AutoMapper;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.MapperProfiles;

public class GeoPointProfile : Profile
{
    public GeoPointProfile()
    {
        CreateMap<CreateGeoPointDto, GeoPointEntity>()
            .ForMember(ent => ent.Type, opt => opt.Ignore())
            .ForMember(ent => ent.Polygon, opt => opt.Ignore())
            .ForMember(ent => ent.PolygonId, opt => opt.Ignore())

            .ForMember(ent => ent.Id, opt => opt.Ignore())
            .ForMember(ent => ent.CreatedDate, opt => opt.Ignore())
            .ForMember(ent => ent.UpdatedDate, opt => opt.Ignore())
            .ForMember(ent => ent.IsDeleted, opt => opt.Ignore())
            ;
        CreateMap<GeoPointEntity, GeoPointDto>();
    }
}
