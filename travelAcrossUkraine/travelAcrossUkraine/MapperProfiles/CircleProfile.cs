using AutoMapper;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.MapperProfiles;

public class CircleProfile : Profile
{
    public CircleProfile()
    {
        CreateMap<CreateCircleDto, CircleEntity>()
            .ForMember(ent => ent.CenterGeoPointId, opt => opt.Ignore())

            .ForMember(ent => ent.Id, opt => opt.Ignore())
            .ForMember(ent => ent.CreatedDate, opt => opt.Ignore())
            .ForMember(ent => ent.UpdatedDate, opt => opt.Ignore())
            .ForMember(ent => ent.IsDeleted, opt => opt.Ignore())
            ;

        CreateMap<CircleEntity, CircleDto>()
            ;
    }
}
