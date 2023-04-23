using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Services;
using TravelAcrossUkraine.WebApi.Utility;
using TravelAcrossUkraine.WebApi.Utility.Validators;

namespace TravelAcrossUkraine.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TypesController : ControllerBase
{
    private readonly ITypeService _typeService;
    private readonly ILogger<TypesController> _logger;

    public TypesController(ITypeService typeService, ILogger<TypesController> logger)
    {
        _typeService = typeService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<TypeDto>>> GetAllAsync()
    {
        try
        {
            return await _typeService.GetAllAsync();
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TypeDto>> GetByIdAsync(Guid id)
    {
        try
        {
            return await _typeService.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Guid>> CreateAsync(CreateTypeDto type)
    {
        try
        {
            Validators.ValidateCreateTypeDto(type);

            return await _typeService.CreateAsync(type);
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<TypeDto>> DeleteAsync(Guid id)
    {
        try
        {
            await _typeService.DeleteAsync(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }
}
