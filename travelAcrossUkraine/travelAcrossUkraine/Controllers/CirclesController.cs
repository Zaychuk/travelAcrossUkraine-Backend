using Microsoft.AspNetCore.Mvc;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Services;
using TravelAcrossUkraine.WebApi.Utility.Validators;

namespace TravelAcrossUkraine.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CirclesController : ControllerBase
{
    private readonly ICircleService _circleService;

    public CirclesController(ICircleService circleService)
    {
        _circleService = circleService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CircleDto>>> GetAllAsync()
    {
        return await _circleService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CircleDto>> GetByIdAsync(Guid id)
    {
        return await _circleService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync(CircleDto circle)
    {
        Validators.ValidateCircleDto(circle);

        return await _circleService.CreateAsync(circle);
    }
}
