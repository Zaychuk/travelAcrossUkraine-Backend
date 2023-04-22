using AutoMapper;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Entities;
using TravelAcrossUkraine.WebApi.Exceptions;
using TravelAcrossUkraine.WebApi.Helpers;
using TravelAcrossUkraine.WebApi.Repositories;

namespace TravelAcrossUkraine.WebApi.Services;

public interface ITypeService
{
    Task<List<TypeDto>> GetAllAsync();
    Task<TypeDto> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(CreateTypeDto typeDto);
    Task DeleteAsync(Guid id);
}

public class TypeService : ITypeService
{
    private readonly ITypeRepository _typeRepository;
    private readonly IMapper _mapper;

    public TypeService(ITypeRepository typeRepository, IMapper mapper)
    {
        _typeRepository = typeRepository;
        _mapper = mapper;
    }

    public async Task<Guid> CreateAsync(CreateTypeDto typeDto)
    {
        var type = _mapper.Map<TypeEntity>(typeDto);
        BaseEntityHelper.SetBaseProperties(type);

        await _typeRepository.CreateAsync(type);

        return type.Id;
    }

    public async Task<List<TypeDto>> GetAllAsync()
    {
        var typeEntities = await _typeRepository.GetAllAsync();

        return typeEntities
            .Select(typeEntity => _mapper.Map<TypeDto>(typeEntity))
            .ToList();
    }

    public async Task<TypeDto> GetByIdAsync(Guid id)
    {
        var typeEntity = await _typeRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Type {id} was not found");

        return _mapper.Map<TypeDto>(typeEntity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var typeEntity = await _typeRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Type {id} was not found");

        await _typeRepository.DeleteAsync(typeEntity);
    }
}
