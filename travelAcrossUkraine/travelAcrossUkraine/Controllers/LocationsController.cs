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
public class LocationsController : ControllerBase
{
    private readonly ILocationService _locationService;
    private readonly ILogger<LocationsController> _logger;

    public LocationsController(ILocationService locationService, ILogger<LocationsController> logger)
    {
        _locationService = locationService;
        _logger = logger;
    }

    [HttpPost("inGivenArea")]
    public async Task<ActionResult<List<LocationDto>>> GetAllInGivenAreaAsync(PolygonDto areaPolygon)
    {
        try
        {
            return await _locationService.GetAllInGivenAreaAsync(areaPolygon);
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    [HttpPost("byFilter")]
    public async Task<ActionResult<List<LocationDto>>> GetAllByProvidedFilterAsync(LocationFilterDto filter)
    {
        try
        {
            return await _locationService.GetAllByProvidedFilterAsync(filter);
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<LocationDto>>> GetAllAsync()
    {
        try
        {
            return await _locationService.GetAllAsync();
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    [HttpGet("pending")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<LocationDto>>> GetAllPendingAsync()
    {
        try
        {
            return await _locationService.GetAllPendingAsync();
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LocationDto>> GetByIdAsync(Guid id)
    {
        try
        {
            return await _locationService.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateLocationDto location)
    {
        try
        {
            Validators.ValidateCreateLocationDto(location);

            return await _locationService.CreateAsync(location);
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    [HttpPut("{id}/approve")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> ApproveAsync(Guid id)
    {
        try
        {
            await _locationService.ApproveAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    [HttpPut("{id}/decline")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeclineAsync(Guid id)
    {
        try
        {
            await _locationService.DeclineAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }


    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        try
        {
            await _locationService.DeleteAsync(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }
}
