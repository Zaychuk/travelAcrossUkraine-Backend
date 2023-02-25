using AutoMapper;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Entities;
using TravelAcrossUkraine.WebApi.Repositories;

namespace TravelAcrossUkraine.WebApi.Services;

public interface ILocationService
{
    Task<List<LocationDto>> GetAllAsync();
    Task<LocationDto> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(CreateLocationDto createLocationDto);
}

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;
    private readonly IMapper _mapper;

    public LocationService(ILocationRepository locationRepository, IMapper mapper)
    {
        _locationRepository = locationRepository;
        _mapper = mapper;
    }

    public async Task<Guid> CreateAsync(CreateLocationDto createLocationDto)
    {
        var location = _mapper.Map<LocationEntity>(createLocationDto);
        location.Id = Guid.NewGuid();
        location.Images = createLocationDto.ImageFiles.Select(imageFile =>
        {
            var ms = new MemoryStream();

            imageFile.CopyTo(ms);
            var imageData = ms.ToArray();

            ms.Close();
            ms.Dispose();

            return new ImageEntity
            {
                Id = Guid.NewGuid(),
                FileName = imageFile.FileName,
                ImageData = imageData
            };
        }).ToList();


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
            locationDtos.Single(location => location.Id.Equals(locationEntity.Id)).Images = locationEntity.Images.Select(image =>
            {
                string imageBase64Data = Convert.ToBase64String(image.ImageData);
                string imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);

                return new ImageWithoutLocationDto
                {
                    Id = image.Id,
                    FileName = image.FileName,
                    ImageDataUrl = imageDataURL
                };
            }).ToList();
        }

        return locationDtos;
    }

    public async Task<LocationDto> GetByIdAsync(Guid id)
    {
        var locationEntity = await _locationRepository.GetByIdAsync(id);

        var locationDto = _mapper.Map<LocationDto>(locationEntity);

        locationDto.Images = locationEntity.Images.Select(image =>
        {
            string imageBase64Data = Convert.ToBase64String(image.ImageData);
            string imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);

            return new ImageWithoutLocationDto
            {
                Id = image.Id,
                FileName = image.FileName,
                ImageDataUrl = imageDataURL
            };
        }).ToList();

        return locationDto;
    }
}
