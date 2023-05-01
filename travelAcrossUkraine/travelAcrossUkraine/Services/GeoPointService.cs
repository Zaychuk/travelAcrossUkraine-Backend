using AutoMapper;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Entities;
using TravelAcrossUkraine.WebApi.Exceptions;
using TravelAcrossUkraine.WebApi.Helpers;
using TravelAcrossUkraine.WebApi.Repositories;

namespace TravelAcrossUkraine.WebApi.Services;

public interface IGeoPointService
{
    Task<List<GeoPointDto>> GetAllAsync();
    Task<Guid> CreateAsync(CreateGeoPointDto geoPoint);
    Task<GeoPointDto> GetByIdAsync(Guid id);
    Task DeleteAsync(Guid id);
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

    public async Task<Guid> CreateAsync(CreateGeoPointDto geoPointDto)
    {
        var geoPoint = _mapper.Map<GeoPointEntity>(geoPointDto);
        BaseEntityHelper.SetBaseProperties(geoPoint);

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
        var geoPoint = await _geoPointRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Geopoint {id} was not found");

        return _mapper.Map<GeoPointDto>(geoPoint);
    }

    public async Task DeleteAsync(Guid id)
    {
        var geoPoint = await _geoPointRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Geopoint {id} was not found");

        await _geoPointRepository.DeleteAsync(geoPoint);
    }
}
