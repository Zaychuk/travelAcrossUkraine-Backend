using Microsoft.AspNetCore.Mvc;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Services;
using TravelAcrossUkraine.WebApi.Utility;
using TravelAcrossUkraine.WebApi.Utility.Validators;

namespace TravelAcrossUkraine.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PolygonsController : ControllerBase
{
    private readonly IPolygonService _polygonService;
    private readonly ILogger<PolygonsController> _logger;

    public PolygonsController(IPolygonService polygonService, ILogger<PolygonsController> logger)
    {
        _polygonService = polygonService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<PolygonDto>>> GetAllAsync()
    {
        try
        {
            return await _polygonService.GetAllAsync();
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    [HttpGet("{polygonId}")]
    public async Task<ActionResult<PolygonDto>> GetByIdAsync(Guid polygonId)
    {
        try
        {
            return await _polygonService.GetByIdAsync(polygonId);
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync(PolygonDto polygon)
    {
        try
        {
            Validators.ValidatePolygonDto(polygon);

            return await _polygonService.CreateAsync(polygon);
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
            await _polygonService.DeleteAsync(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }
}
