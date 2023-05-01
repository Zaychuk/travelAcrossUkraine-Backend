using AutoMapper;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.MapperProfiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CreateCategoryDto, CategoryEntity>()
            .ForMember(ent => ent.Type, opt => opt.Ignore())
            .ForMember(ent => ent.Locations, opt => opt.Ignore())

            .ForMember(ent => ent.Id, opt => opt.Ignore())
            .ForMember(ent => ent.CreatedDate, opt => opt.Ignore())
            .ForMember(ent => ent.UpdatedDate, opt => opt.Ignore())
            .ForMember(ent => ent.IsDeleted, opt => opt.Ignore())
            ;

        CreateMap<CategoryEntity, CategoryDto>()
            .ForMember(ent => ent.Type, opt => opt.Ignore())
            ;

        CreateMap<CategoryEntity, CategoryWithoutTypeDto>();
    }
}
