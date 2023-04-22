using AutoMapper;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Entities;
using TravelAcrossUkraine.WebApi.Exceptions;
using TravelAcrossUkraine.WebApi.Helpers;
using TravelAcrossUkraine.WebApi.Repositories;

namespace TravelAcrossUkraine.WebApi.Services;

public interface IPolygonService
{
    Task<List<PolygonDto>> GetAllAsync();
    Task<PolygonDto> GetByIdAsync(Guid polygonId);
    Task<Guid> CreateAsync(PolygonDto polygon);
    Task DeleteAsync(Guid polygonId);
}
public class PolygonService : IPolygonService
{
    private readonly IPolygonRepository _polygonRepository;
    private readonly IMapper _mapper;

    public PolygonService(IPolygonRepository polygonRepository, IMapper mapper)
    {
        _polygonRepository = polygonRepository;
        _mapper = mapper;
    }

    public async Task<Guid> CreateAsync(PolygonDto polygonDto)
    {
        var polygon = _mapper.Map<PolygonEntity>(polygonDto);
        BaseEntityHelper.SetBaseProperties(polygon);

        // TODO: move to PolygonHelper when one created
        polygon.GeoPoints = polygon.GeoPoints.Select((geoPoint, index) =>
        {
            geoPoint.SequenceNumber = index;
            BaseEntityHelper.SetBaseProperties(geoPoint);
            return geoPoint;
        }).ToList();

        await _polygonRepository.CreateAsync(polygon);

        return polygon.Id;
    }

    public async Task<List<PolygonDto>> GetAllAsync()
    {
        var polygons = await _polygonRepository.GetAllAsync();

        return polygons.Select(polygon => _mapper.Map<PolygonDto>(polygon)).ToList();
    }

    public async Task<PolygonDto> GetByIdAsync(Guid polygonId)
    {
        var polygon = await _polygonRepository.GetByIdAsync(polygonId) ?? throw new NotFoundException($"Polygon {polygonId} was not found");

        return _mapper.Map<PolygonDto>(polygon);
    }

    public async Task DeleteAsync(Guid polygonId)
    {
        var polygon = await _polygonRepository.GetByIdAsync(polygonId) ?? throw new NotFoundException($"Polygon {polygonId} was not found");

        await _polygonRepository.DeleteAsync(polygon);
    }
}
