using Microsoft.VisualBasic;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Dtos.Auth;
using TravelAcrossUkraine.WebApi.Helpers;

namespace TravelAcrossUkraine.WebApi.Utility.Validators;

public static class Validators
{
    public static void ValidateCreateCategoryDto(CreateCategoryDto createCategoryDto)
    {
        if (createCategoryDto == null
            || string.IsNullOrWhiteSpace(createCategoryDto.Name)
            || createCategoryDto.TypeId == Guid.Empty)
        {
            throw new BadHttpRequestException($"{nameof(CreateCategoryDto)}: {ErrorMessages.NotAllRequiredDataProvided}");
        }
    }

    public static void ValidateCircleDto(CreateCircleDto circleDto)
    {
        if (circleDto == null
            || circleDto.Radius <= 0
            || circleDto.CenterGeoPoint == null)
        {
            throw new BadHttpRequestException($"{nameof(CircleDto)}: {ErrorMessages.NotAllRequiredDataProvided}");
        }
    }

    public static void ValidatePolygonDto(CreatePolygonDto polygonDto)
    {
        if (polygonDto == null
            || polygonDto.GeoPoints == null
            || !polygonDto.GeoPoints.Any())
        {
            throw new BadHttpRequestException($"{nameof(PolygonDto)}: {ErrorMessages.NotAllRequiredDataProvided}");
        }
    }

    public static void ValidateGeoPointDto(CreateGeoPointDto geoPointDto)
    {
        if (geoPointDto == null)
        {
            throw new BadHttpRequestException($"{nameof(GeoPointDto)}: {ErrorMessages.NotAllRequiredDataProvided}");
        }
    }

    public static void ValidateCreateTypeDto(CreateTypeDto createTypeDto)
    {
        if (createTypeDto == null
            || string.IsNullOrWhiteSpace(createTypeDto.Name))
        {
            throw new BadHttpRequestException($"{nameof(CreateTypeDto)}: {ErrorMessages.NotAllRequiredDataProvided}");
        }
    }

    public static void ValidateCreateLocationDto(CreateLocationDto createLocationDto)
    {
        if (createLocationDto == null
            || createLocationDto.ImageFiles == null
            || !createLocationDto.ImageFiles.Any()
            || createLocationDto.CategoryId == Guid.Empty
            || string.IsNullOrWhiteSpace(createLocationDto.Description)
            || string.IsNullOrWhiteSpace(createLocationDto.Name)
            || (createLocationDto.GeoPoint == null && createLocationDto.Polygon == null && createLocationDto.Circle == null))
        {
            throw new BadHttpRequestException($"{nameof(CreateLocationDto)}: {ErrorMessages.NotAllRequiredDataProvided}");
        }
    }

    public static void ValidateCreateUserDto(CreateUserDto createUserDto)
    {
        if (createUserDto == null
            || string.IsNullOrWhiteSpace(createUserDto.Username)
            || createUserDto.Username.Length > Constants.MaxUsernameLength
            || string.IsNullOrWhiteSpace(createUserDto.Password)
            || string.IsNullOrWhiteSpace(createUserDto.EmailAddress)
            || !EmailHelper.IsValid(createUserDto.EmailAddress))
        {
            throw new BadHttpRequestException($"{nameof(CreateUserDto)}: {ErrorMessages.NotAllRequiredDataProvided}");
        }
    }

    public static void ValidateUserLoginDto(UserLoginDto userLogin)
    {
        if (userLogin == null
            || string.IsNullOrWhiteSpace(userLogin.Username)
            || string.IsNullOrWhiteSpace(userLogin.Password))
        {
            throw new BadHttpRequestException($"{nameof(UserLoginDto)}: {ErrorMessages.NotAllRequiredDataProvided}");
        }
    }
}
