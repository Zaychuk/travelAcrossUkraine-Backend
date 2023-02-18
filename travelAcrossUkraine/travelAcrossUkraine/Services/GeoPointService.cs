using AutoMapper;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Entities;
using TravelAcrossUkraine.WebApi.Repositories;

namespace TravelAcrossUkraine.WebApi.Services;

public interface IGeoPointService
{
    Task<List<GeoPointDto>> GetAllAsync();
    Task<Guid> CreateAsync(GeoPointDto geoPoint);
    Task<GeoPointDto> GetByIdAsync(Guid id);
}

public class GeoPointService : IGeoPointService
{
    private readonly IGeoPointRepository _geoPointRepository;
    private readonly IMapper _mapper;

    public GeoPointService(IGeoPointRepository geoPointRepository, IMapper mapper)
    {
        _geoPointRepository = geoPointRepository;
        _mapper = mapper;
    }

    public async Task<Guid> CreateAsync(GeoPointDto geoPointDto)
    {
        var geoPoint = _mapper.Map<GeoPointEntity>(geoPointDto);
        geoPoint.Id = Guid.NewGuid();
        await _geoPointRepository.CreateAsync(geoPoint);

        return geoPoint.Id;
    }

    public async Task<List<GeoPointDto>> GetAllAsync()
    {
        var geoPoints = await _geoPointRepository.GetAllAsync();

        return geoPoints.Select(geoPoint => _mapper.Map<GeoPointDto>(geoPoint)).ToList();
    }

    public async Task<GeoPointDto> GetByIdAsync(Guid id)
    {
        var geoPoint = await _geoPointRepository.GetByIdAsync(id); 

        return _mapper.Map<GeoPointDto>(geoPoint);
    }
}
