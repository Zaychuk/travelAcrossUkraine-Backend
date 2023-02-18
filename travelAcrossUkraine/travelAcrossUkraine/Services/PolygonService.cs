using AutoMapper;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Entities;
using TravelAcrossUkraine.WebApi.Repositories;

namespace TravelAcrossUkraine.WebApi.Services;

public interface IPolygonService
{
    Task<List<PolygonDto>> GetAllAsync();
    Task<PolygonDto> GetByIdAsync(Guid polygonId);
    Task<Guid> CreateAsync(PolygonDto polygon);
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
        polygon.Id = Guid.NewGuid();
        polygon.GeoPoints = polygon.GeoPoints.Select((geoPoint, index) =>
        {
            geoPoint.Id = Guid.NewGuid();
            geoPoint.SequenceNumber = index;
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
        var polygon = await _polygonRepository.GetByIdAsync(polygonId);
        var abc = _mapper.Map<PolygonDto>(polygon);
        return abc;
    }
}
