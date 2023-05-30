using AutoMapper;
using System.Linq.Expressions;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Entities;
using TravelAcrossUkraine.WebApi.Exceptions;
using TravelAcrossUkraine.WebApi.Helpers;
using TravelAcrossUkraine.WebApi.Repositories;
using TravelAcrossUkraine.WebApi.Utility.Enums;

namespace TravelAcrossUkraine.WebApi.Services;

public interface ILocationService
{
    Task<List<LocationDto>> GetAllInGivenAreaAsync(PolygonDto areaPolygon);
    Task<List<LocationDto>> GetAllAsync();
    Task<LocationDto> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(CreateLocationDto createLocationDto);
    Task DeleteAsync(Guid id);
    Task<List<LocationDto>> GetAllPendingAsync();
    Task DeclineAsync(Guid id);
    Task ApproveAsync(Guid id);
    Task<List<LocationDto>> GetAllByProvidedFilterAsync(LocationFilterDto filterDto);
}

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;
    private readonly IImageRepository _imageRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public LocationService(ILocationRepository locationRepository, IImageRepository imageRepository,
        IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _locationRepository = locationRepository;
        _imageRepository = imageRepository;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task<List<LocationDto>> GetAllByProvidedFilterAsync(LocationFilterDto filterDto)
    {
        Expression<Func<LocationEntity, bool>> condition = location =>
               (string.IsNullOrEmpty(filterDto.Term) ||
               location.Name.Contains(filterDto.Term) ||
               location.Description.Contains(filterDto.Term)
               )
               && (filterDto.CategoryId == null || location.CategoryId == filterDto.CategoryId);

        var locations = await _locationRepository.GetByConditionAsync(condition);

        return locations
            .Select(location => _mapper.Map<LocationDto>(location))
            .ToList();

    }

    public async Task<List<LocationDto>> GetAllInGivenAreaAsync(PolygonDto areaPolygon)
    {
        var locations = await _locationRepository.GetAllAsync();

        var locationDtos = locations
            .Where(location => location.GeoPoint != null)
            .Select(location => _mapper.Map<LocationDto>(location))
            .ToList();

        locationDtos.ForEach(location =>
        {
            if (location.GeoPoint != null)
            {
                if (!AreaHelper.CheckIfGeoPointInsidePolygon(areaPolygon, areaPolygon.GeoPoints.Count, location.GeoPoint))
                {
                    locationDtos.Remove(location);
                }
            }
            if (location.Circle != null)
            {
                if (!AreaHelper.CheckIfPolygonAndCirleIntersect(areaPolygon, location.Circle))
                {
                    locationDtos.Remove(location);
                }
            }
            if (location.Polygon != null)
            {
                if (!AreaHelper.CheckIfPolygonsIntersect(areaPolygon, location.Polygon))
                {
                    locationDtos.Remove(location);
                }
            }
        });

        return locationDtos;
    }

    public async Task<Guid> CreateAsync(CreateLocationDto createLocationDto)
    {
        var location = _mapper.Map<LocationEntity>(createLocationDto);
        BaseEntityHelper.SetBaseProperties(location);
        var user = AuthenticatedUserHelper.GetAuthenticatedUser(_httpContextAccessor.HttpContext.User.Identity);

        if (user.Role == "Admin")
        {
            location.Status = LocationStatuses.Approved;
        }

        var images = createLocationDto.ImageUrls.Select(imageUrl =>
        {

            var imageToCreate = new ImageEntity
            {
                Url = imageUrl,
                LocationId = location.Id
            };
            BaseEntityHelper.SetBaseProperties(imageToCreate);

            return imageToCreate;
        }).ToList();
        await _imageRepository.CreateRangeAsync(images);

        if (location.GeoPoint != null)
        {
            BaseEntityHelper.SetBaseProperties(location.GeoPoint);
        }
        else if (location.Polygon != null)
        {
            BaseEntityHelper.SetBaseProperties(location.Polygon);

            // TODO: move to PolygonHelper when one created
            location.Polygon.GeoPoints = location.Polygon.GeoPoints.Select((geoPoint, index) =>
            {
                geoPoint.SequenceNumber = index;
                BaseEntityHelper.SetBaseProperties(geoPoint);
                return geoPoint;
            }).ToList();
        }
        else if (location.Circle != null)
        {
            BaseEntityHelper.SetBaseProperties(location.Circle);
            BaseEntityHelper.SetBaseProperties(location.Circle.CenterGeoPoint);
        }

        await _locationRepository.CreateAsync(location);

        return location.Id;
    }

    public async Task<List<LocationDto>> GetAllAsync()
    {
        var locationEntities = await _locationRepository.GetAllAsync();

        var locationDtos = locationEntities
            .Select(locationEntity => _mapper.Map<LocationDto>(locationEntity))
            .ToList();

        return locationDtos;
    }

    public async Task<List<LocationDto>> GetAllPendingAsync()
    {
        var locationEntities = await _locationRepository.GetAllPendingAsync();

        var locationDtos = locationEntities
            .Select(locationEntity => _mapper.Map<LocationDto>(locationEntity))
            .ToList();

        return locationDtos;
    }

    public async Task<LocationDto> GetByIdAsync(Guid id)
    {
        var locationEntity = await _locationRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Location {id} was not found");

        var locationDto = _mapper.Map<LocationDto>(locationEntity);

        return locationDto;
    }

    public async Task ApproveAsync(Guid id)
    {
        var locationEntity = await _locationRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Location {id} was not found");
        locationEntity.Status = LocationStatuses.Approved;
        await _locationRepository.UpdateAsync(locationEntity);
    }

    public async Task DeclineAsync(Guid id)
    {
        var locationEntity = await _locationRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Location {id} was not found");
        locationEntity.Status = LocationStatuses.Declined;
        await _locationRepository.UpdateAsync(locationEntity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var locationEntity = await _locationRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Location {id} was not found");

        await _locationRepository.DeleteAsync(locationEntity);
    }
}
