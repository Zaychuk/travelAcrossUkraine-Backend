using Microsoft.AspNetCore.Mvc;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Services;
using TravelAcrossUkraine.WebApi.Utility.Validators;

namespace TravelAcrossUkraine.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> GetAllAsync()
    {
        return await _categoryService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetByIdAsync(Guid id)
    {
        return await _categoryService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync(CreateCategoryDto category)
    {
        Validators.ValidateCreateCategoryDto(category);

        return await _categoryService.CreateAsync(category);
    }
}
