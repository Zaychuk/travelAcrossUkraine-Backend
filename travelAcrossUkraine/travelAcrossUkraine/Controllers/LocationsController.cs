using Microsoft.AspNetCore.Mvc;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Services;

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
    public async Task<ActionResult<List<PolygonDto>>> GetAllAsync()
    {
        var locations = await _locationService.GetAllAsync();
        return Ok(locations);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PolygonDto>> GetByIdAsync(Guid id)
    {
        var location = await _locationService.GetByIdAsync(id);
        return Ok(location);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync([FromForm] CreateLocationDto location)
    {
        var id = await _locationService.CreateAsync(location);
        return Ok(id);
    }
}
