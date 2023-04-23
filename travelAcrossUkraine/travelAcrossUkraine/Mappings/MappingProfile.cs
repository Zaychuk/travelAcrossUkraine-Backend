using AutoMapper;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Category
        CreateMap<CategoryEntity, CreateCategoryDto>().ReverseMap();
        CreateMap<CategoryEntity, CategoryDto>().ReverseMap();
        CreateMap<CategoryEntity, CategoryWithoutTypeDto>().ReverseMap();

        // Circle
        CreateMap<CircleEntity, CircleDto>().ReverseMap();

        // GeoPoint
        CreateMap<GeoPointEntity, GeoPointDto>().ReverseMap();

        // Image
        //CreateMap<ImageEntity, ImageWithoutLocationDto>().ReverseMap();

        // Location
        CreateMap<LocationEntity, LocationDto>()
            .ForMember(x => x.Images, opt => opt.Ignore()).ReverseMap();
        CreateMap<LocationEntity, CreateLocationDto>().ReverseMap();

        // Polygon
        CreateMap<PolygonEntity, PolygonDto>().ReverseMap();

        // Type
        CreateMap<TypeEntity, CreateTypeDto>().ReverseMap();
        CreateMap<TypeEntity, TypeDto>().ReverseMap();
        CreateMap<TypeEntity, TypeWithoutCategoryDto>().ReverseMap();

        // User
        CreateMap<CreateUserDto, UserEntity>()
            .ForMember(x => x.PasswordHash, opt => opt.Ignore())
            .ForMember(x => x.RoleId, opt => opt.Ignore())
            .ForMember(x => x.Role, opt => opt.Ignore());
        CreateMap<UserEntity, UserDto>().ReverseMap();
    }
}
