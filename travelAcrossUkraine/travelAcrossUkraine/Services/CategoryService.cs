using AutoMapper;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Entities;
using TravelAcrossUkraine.WebApi.Helpers;
using TravelAcrossUkraine.WebApi.Repositories;

namespace TravelAcrossUkraine.WebApi.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync();
    Task<CategoryDto> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(CreateCategoryDto categoryDto);
    Task DeleteAsync(Guid id);
    Task<Guid> UpdateAsync(Guid id, CreateCategoryDto categoryDto);
}

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<List<CategoryDto>> GetAllAsync()
    {
        var categoryEntities = await _categoryRepository.GetAllAsync();

        return categoryEntities
            .Select(categoryEntity => _mapper.Map<CategoryDto>(categoryEntity))
            .ToList();
    }

    public async Task<CategoryDto> GetByIdAsync(Guid id)
    {
        var categoryEntity = await _categoryRepository.GetByIdAsync(id);

        return _mapper.Map<CategoryDto>(categoryEntity);
    }

    public async Task<Guid> CreateAsync(CreateCategoryDto categoryDto)
    {
        var category = _mapper.Map<CategoryEntity>(categoryDto);
        BaseEntityHelper.SetBaseProperties(category);

        await _categoryRepository.CreateAsync(category);

        return category.Id;
    }

    public async Task<Guid> UpdateAsync(Guid id, CreateCategoryDto categoryDto)
    {
        var categoryEntity = await _categoryRepository.GetByIdAsync(id) ?? throw new BadHttpRequestException($"Category {id} has not been found");
        categoryEntity = _mapper.Map(categoryDto, categoryEntity);
        BaseEntityHelper.UpdateBaseProperties(categoryEntity);

        await _categoryRepository.UpdateAsync(categoryEntity);

        return categoryEntity.Id;
    }

    public async Task DeleteAsync(Guid id)
    {
        var categoryEntity = await _categoryRepository.GetByIdAsync(id) ?? throw new BadHttpRequestException($"Category {id} has not been found");

        await _categoryRepository.DeleteAsync(categoryEntity);
    }
}
