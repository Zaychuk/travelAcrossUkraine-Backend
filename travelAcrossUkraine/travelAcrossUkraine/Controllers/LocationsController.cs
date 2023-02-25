using Microsoft.AspNetCore.Mvc;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Services;
using TravelAcrossUkraine.WebApi.Utility.Validators;

namespace TravelAcrossUkraine.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationsController : ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationsController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet]
    public async Task<ActionResult<List<LocationDto>>> GetAllAsync()
    {
        return await _locationService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LocationDto>> GetByIdAsync(Guid id)
    {
        return await _locationService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync([FromForm] CreateLocationDto location)
    {
        Validators.ValidateCreateLocationDto(location);

        return await _locationService.CreateAsync(location);
    }
}
