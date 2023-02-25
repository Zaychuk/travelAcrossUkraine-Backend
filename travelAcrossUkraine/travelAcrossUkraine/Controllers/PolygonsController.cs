using Microsoft.AspNetCore.Mvc;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Services;
using TravelAcrossUkraine.WebApi.Utility.Validators;

namespace TravelAcrossUkraine.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PolygonsController : ControllerBase
{
    private readonly IPolygonService _polygonService;

    public PolygonsController(IPolygonService polygonService)
    {
        _polygonService = polygonService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PolygonDto>>> GetAllAsync()
    {
        return await _polygonService.GetAllAsync();
    }

    [HttpGet("{polygonId}")]
    public async Task<ActionResult<PolygonDto>> GetByIdAsync(Guid polygonId)
    {
        return await _polygonService.GetByIdAsync(polygonId);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync(PolygonDto polygon)
    {
        Validators.ValidatePolygonDto(polygon);

        return await _polygonService.CreateAsync(polygon);
    }
}
