using Microsoft.AspNetCore.Mvc;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Services;
using TravelAcrossUkraine.WebApi.Utility;
using TravelAcrossUkraine.WebApi.Utility.Validators;

namespace TravelAcrossUkraine.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> GetAllAsync()
    {
        try
        {
            return await _categoryService.GetAllAsync();
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetByIdAsync(Guid id)
    {
        try
        {
            return await _categoryService.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync(CreateCategoryDto category)
    {
        try
        {
            Validators.ValidateCreateCategoryDto(category);

            return await _categoryService.CreateAsync(category);

        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        try
        {
            await _categoryService.DeleteAsync(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }
}
