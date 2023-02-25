using Microsoft.AspNetCore.Mvc;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Services;
using TravelAcrossUkraine.WebApi.Utility.Validators;

namespace TravelAcrossUkraine.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TypesController : ControllerBase
{
    private readonly ITypeService _typeService;

    public TypesController(ITypeService typeService)
	{
        _typeService = typeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TypeDto>>> GetAllAsync()
    {
        return await _typeService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TypeDto>> GetByIdAsync(Guid id)
    {
        return await _typeService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync(CreateTypeDto type)
    {
        Validators.ValidateCreateTypeDto(type);

        return await _typeService.CreateAsync(type);
    }
}
