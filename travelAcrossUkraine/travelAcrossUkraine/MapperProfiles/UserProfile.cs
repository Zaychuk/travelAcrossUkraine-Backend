using AutoMapper;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.MapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, UserEntity>()
            .ForMember(x => x.PasswordHash, opt => opt.Ignore())
            .ForMember(x => x.RoleId, opt => opt.Ignore())
            .ForMember(x => x.Role, opt => opt.Ignore())
            .ForMember(x => x.Collections, opt => opt.Ignore())

            .ForMember(ent => ent.Id, opt => opt.Ignore())
            .ForMember(ent => ent.CreatedDate, opt => opt.Ignore())
            .ForMember(ent => ent.UpdatedDate, opt => opt.Ignore())
            .ForMember(ent => ent.IsDeleted, opt => opt.Ignore())
            ;

        CreateMap<UserEntity, UserDto>()
            .ForMember(dto => dto.Role, opt => opt.MapFrom(ent => ent.Role.Name));
    }
}
