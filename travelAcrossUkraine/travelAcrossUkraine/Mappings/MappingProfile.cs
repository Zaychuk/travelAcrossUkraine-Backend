using AutoMapper;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // GeoPoint
        CreateMap<GeoPointEntity, GeoPointDto>().ReverseMap();

        // Polygon
        CreateMap<PolygonEntity, PolygonDto>().ReverseMap();

        // Circle
        CreateMap<CircleEntity, CircleDto>().ReverseMap();

        // Type
        CreateMap<TypeEntity, CreateTypeDto>().ReverseMap();
        CreateMap<TypeEntity, TypeDto>().ReverseMap();
        CreateMap<TypeEntity, TypeWithoutCategoryDto>().ReverseMap();

        // Category
        CreateMap<CategoryEntity, CreateCategoryDto>().ReverseMap();
        CreateMap<CategoryEntity, CategoryDto>().ReverseMap();
        CreateMap<CategoryEntity, CategoryWithoutTypeDto>().ReverseMap();

    }
}
