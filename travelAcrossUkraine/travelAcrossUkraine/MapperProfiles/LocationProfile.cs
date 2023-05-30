using AutoMapper;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.MapperProfiles;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<LocationEntity, LocationDto>()
            .ForMember(ent => ent.ImageUrls, opt => opt.MapFrom(ent => ent.Images.Select(i => i.Url)))
            .ForMember(ent => ent.EcologicalProblems, opt => opt.Ignore())
            ;

        CreateMap<CreateLocationDto, LocationEntity>()
            .ForMember(ent => ent.Status, opt => opt.Ignore())
            .ForMember(ent => ent.Images, opt => opt.Ignore())
            .ForMember(ent => ent.Category, opt => opt.Ignore())
            .ForMember(ent => ent.PolygonId, opt => opt.Ignore())
            .ForMember(ent => ent.CircleId, opt => opt.Ignore())
            .ForMember(ent => ent.GeoPointId, opt => opt.Ignore())
            .ForMember(ent => ent.CollectionLocations, opt => opt.Ignore())

            .ForMember(ent => ent.Id, opt => opt.Ignore())
            .ForMember(ent => ent.CreatedDate, opt => opt.Ignore())
            .ForMember(ent => ent.UpdatedDate, opt => opt.Ignore())
            .ForMember(ent => ent.IsDeleted, opt => opt.Ignore())
            ;
    }
}
