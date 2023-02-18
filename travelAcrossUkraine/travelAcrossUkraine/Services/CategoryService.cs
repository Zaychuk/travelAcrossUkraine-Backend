using AutoMapper;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Entities;
using TravelAcrossUkraine.WebApi.Repositories;

namespace TravelAcrossUkraine.WebApi.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync();
    Task<CategoryDto> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(CreateCategoryDto categoryDto);
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
    public async Task<Guid> CreateAsync(CreateCategoryDto categoryDto)
    {
        var category = _mapper.Map<CategoryEntity>(categoryDto);
        category.Id = Guid.NewGuid();

        await _categoryRepository.CreateAsync(category);

        return category.Id;
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
}
