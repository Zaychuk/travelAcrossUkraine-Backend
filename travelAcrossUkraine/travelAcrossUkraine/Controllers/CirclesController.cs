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
public class CirclesController : ControllerBase
{
    private readonly ICircleService _circleService;
    private readonly ILogger<CirclesController> _logger;

    public CirclesController(ICircleService circleService, ILogger<CirclesController> logger)
    {
        _circleService = circleService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<CircleDto>>> GetAllAsync()
    {
        try
        {
            return await _circleService.GetAllAsync();
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CircleDto>> GetByIdAsync(Guid id)
    {
        try
        {
            return await _circleService.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Guid>> CreateAsync(CreateCircleDto circle)
    {
        try
        {
            Validators.ValidateCircleDto(circle);

            return await _circleService.CreateAsync(circle);
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
            await _circleService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }
}
