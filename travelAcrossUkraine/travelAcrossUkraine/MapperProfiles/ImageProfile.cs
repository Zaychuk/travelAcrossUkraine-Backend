using AutoMapper;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.MapperProfiles;

public class ImageProfile:Profile
{
    public ImageProfile()
    {
         CreateMap<ImageEntity, ImageWithoutLocationDto>();
    }
}
