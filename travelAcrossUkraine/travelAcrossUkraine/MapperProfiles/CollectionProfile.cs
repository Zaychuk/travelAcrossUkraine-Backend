using AutoMapper;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.MapperProfiles;

public class CollectionProfile : Profile
{
    public CollectionProfile()
    {
        CreateMap<CollectionEntity, CollectionDto>()
            .ForMember(dto => dto.Locations, opt => opt.MapFrom(ent => ent.CollectionLocations.Select(cl => cl.Location).ToList()));

        CreateMap<CreateCollectionDto, CollectionEntity>()
            .ForMember(ent => ent.Id, opt => opt.Ignore())
            .ForMember(ent => ent.CreatedDate, opt => opt.Ignore())
            .ForMember(ent => ent.UpdatedDate, opt => opt.Ignore())
            .ForMember(ent => ent.IsDeleted, opt => opt.Ignore())

            .ForMember(ent => ent.User, opt => opt.Ignore())
            .ForMember(ent => ent.UserId, opt => opt.Ignore())
            .ForMember(ent => ent.CollectionLocations, opt => opt.Ignore())
            ;
    }
}
