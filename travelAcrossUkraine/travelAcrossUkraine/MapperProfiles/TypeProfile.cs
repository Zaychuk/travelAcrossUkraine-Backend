using AutoMapper;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.MapperProfiles;

public class TypeProfile : Profile
{
    public TypeProfile()
    {
        // Type
        CreateMap<CreateTypeDto, TypeEntity>()
            .ForMember(ent => ent.Categories, opt => opt.Ignore())

            .ForMember(ent => ent.Id, opt => opt.Ignore())
            .ForMember(ent => ent.CreatedDate, opt => opt.Ignore())
            .ForMember(ent => ent.UpdatedDate, opt => opt.Ignore())
            .ForMember(ent => ent.IsDeleted, opt => opt.Ignore())
            ;

        CreateMap<TypeEntity, TypeDto>();
        CreateMap<TypeEntity, TypeWithoutCategoryDto>();
    }
}
