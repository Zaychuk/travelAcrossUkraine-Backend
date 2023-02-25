using TravelAcrossUkraine.WebApi.Dtos;

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

    public static void ValidateCircleDto(CircleDto circleDto)
    {
        if (circleDto == null
            || circleDto.Radius <= 0
            || circleDto.CenterGeoPoint == null)
        {
            throw new BadHttpRequestException($"{nameof(CircleDto)}: {ErrorMessages.NotAllRequiredDataProvided}");
        }
    }

    public static void ValidatePolygonDto(PolygonDto polygonDto)
    {
        if (polygonDto == null
            || polygonDto.GeoPoints == null
            || !polygonDto.GeoPoints.Any())
        {
            throw new BadHttpRequestException($"{nameof(PolygonDto)}: {ErrorMessages.NotAllRequiredDataProvided}");
        }
    }

    public static void ValidateGeoPointDto(GeoPointDto geoPointDto)
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
            || string.IsNullOrWhiteSpace(createLocationDto.Name))
        {
            throw new BadHttpRequestException($"{nameof(CreateLocationDto)}: {ErrorMessages.NotAllRequiredDataProvided}");
        }
    }
}
