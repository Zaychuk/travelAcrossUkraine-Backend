using AutoMapper;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Entities;
using TravelAcrossUkraine.WebApi.Exceptions;
using TravelAcrossUkraine.WebApi.Helpers;
using TravelAcrossUkraine.WebApi.Repositories;

namespace TravelAcrossUkraine.WebApi.Services;

public interface ILocationService
{
    Task<List<LocationDto>> GetAllAsync();
    Task<LocationDto> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(CreateLocationDto createLocationDto);
    Task DeleteAsync(Guid id);
}

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;
    private readonly IGeoPointService _geoPointService;
    private readonly IPolygonService _polygonService;
    private readonly ICircleService _circleService;
    private readonly IMapper _mapper;

    public LocationService(
        ILocationRepository locationRepository,
        IGeoPointService geoPointService,
        IPolygonService polygonService,
        ICircleService circleService,
        IMapper mapper)
    {
        _locationRepository = locationRepository;
        _geoPointService = geoPointService;
        _polygonService = polygonService;
        _circleService = circleService;
        _mapper = mapper;
    }

    public async Task<Guid> CreateAsync(CreateLocationDto createLocationDto)
    {
        var location = _mapper.Map<LocationEntity>(createLocationDto);
        BaseEntityHelper.SetBaseProperties(location);

        location.Images = createLocationDto.ImageFiles.Select(imageFile =>
        {
            var ms = new MemoryStream();

            imageFile.CopyTo(ms);
            var imageData = ms.ToArray();

            ms.Close();
            ms.Dispose();

            var imageToCreate = new ImageEntity
            {
                FileName = imageFile.FileName,
                ImageData = imageData
            };
            BaseEntityHelper.SetBaseProperties(imageToCreate);

            return imageToCreate;
        }).ToList();

        if (location.GeoPoint != null)
        {
            BaseEntityHelper.SetBaseProperties(location.GeoPoint);
        }
        if (location.Polygon != null)
        {
            BaseEntityHelper.SetBaseProperties(location.Polygon);
        }
        if (location.Circle != null)
        {
            BaseEntityHelper.SetBaseProperties(location.Circle);
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

        foreach (var locationEntity in locationEntities)
        {
            var imageWithoutLocationDtos = locationEntity.Images.Select(image => MapImageEntityToImageWithoutLocationDto(image)).ToList();
            locationDtos.Single(location => location.Id.Equals(locationEntity.Id)).Images = imageWithoutLocationDtos;
        }

        return locationDtos;
    }

    public async Task<LocationDto> GetByIdAsync(Guid id)
    {
        var locationEntity = await _locationRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Location {id} was not found");

        var locationDto = _mapper.Map<LocationDto>(locationEntity);

        locationDto.Images = locationEntity.Images
            .Select(image => MapImageEntityToImageWithoutLocationDto(image))
            .ToList();

        return locationDto;
    }

    public async Task DeleteAsync(Guid id)
    {
        var locationEntity = await _locationRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Location {id} was not found");

        await _locationRepository.DeleteAsync(locationEntity);
    }

    private static ImageWithoutLocationDto MapImageEntityToImageWithoutLocationDto(ImageEntity imageEntity)
    {
        string imageBase64Data = Convert.ToBase64String(imageEntity.ImageData);
        string imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);

        return new ImageWithoutLocationDto
        {
            Id = imageEntity.Id,
            FileName = imageEntity.FileName,
            ImageDataUrl = imageDataURL
        };
    }
}
