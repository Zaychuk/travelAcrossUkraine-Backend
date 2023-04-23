using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Services;
using TravelAcrossUkraine.WebApi.Utility;
using TravelAcrossUkraine.WebApi.Utility.Validators;

namespace TravelAcrossUkraine.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class GeoPointsController : ControllerBase
{
    private readonly IGeoPointService _geoPointService;
    private readonly ILogger<GeoPointsController> _logger;

    public GeoPointsController(IGeoPointService geoPointService, ILogger<GeoPointsController> logger)
    {
        _geoPointService = geoPointService;
        _logger = logger;
    }

    /// <summary>
    /// Returns all geopoint on the map
    /// </summary>
    /// <returns>
    /// Returns all geopoint on the map
    /// </returns>
    [HttpGet()]
    public async Task<ActionResult<List<GeoPointDto>>> GetAllAsync()
    {
        try
        {
            return await _geoPointService.GetAllAsync();
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    /// <summary>
    /// Return geopoint by provided id
    /// </summary>
    /// <param name="id">Id of the geopoint to retrieve</param>
    /// <returns>Geopoint by provided id</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<GeoPointDto>> GetAllAsync(Guid id)
    {
        try
        {
            return await _geoPointService.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Guid>> CreateAsync(GeoPointDto geoPoint)
    {
        try
        {
            Validators.ValidateGeoPointDto(geoPoint);

            return await _geoPointService.CreateAsync(geoPoint);
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Guid>> DeleteAsync(Guid id)
    {
        try
        {
            await _geoPointService.DeleteAsync(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

}
