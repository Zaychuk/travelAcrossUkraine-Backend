using AutoMapper;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Entities;
using TravelAcrossUkraine.WebApi.Exceptions;
using TravelAcrossUkraine.WebApi.Helpers;
using TravelAcrossUkraine.WebApi.Repositories;

namespace TravelAcrossUkraine.WebApi.Services;

public interface ICircleService
{
    Task<List<CircleDto>> GetAllAsync();
    Task<CircleDto> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(CreateCircleDto circle);
    Task DeleteAsync(Guid id);
}

public class CircleService : ICircleService
{
    private readonly ICircleRepository _circleRepository;
    private readonly IMapper _mapper;

    public CircleService(ICircleRepository circleRepository, IMapper mapper)
    {
        _circleRepository = circleRepository;
        _mapper = mapper;
    }

    public async Task<Guid> CreateAsync(CreateCircleDto circleDto)
    {
        var circle = _mapper.Map<CircleEntity>(circleDto);

        BaseEntityHelper.SetBaseProperties(circle);
        BaseEntityHelper.SetBaseProperties(circle.CenterGeoPoint);

        await _circleRepository.CreateAsync(circle);

        return circle.Id;
    }

    public async Task<List<CircleDto>> GetAllAsync()
    {
        var circleEntities = await _circleRepository.GetAllAsync();

        return circleEntities
            .Select(circleEntity => _mapper.Map<CircleDto>(circleEntity))
            .ToList();
    }

    public async Task<CircleDto> GetByIdAsync(Guid id)
    {
        var circleEntity = await _circleRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Circle {id} was not found");

        return _mapper.Map<CircleDto>(circleEntity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var circleEntity = await _circleRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Circle {id} was not found");

        await _circleRepository.DeleteAsync(circleEntity);
    }
}
