using Microsoft.AspNetCore.Mvc;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Services;

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
        var polygons = await _polygonService.GetAllAsync();
        return Ok(polygons);
    }

    [HttpGet("{polygonId}")]
    public async Task<ActionResult<PolygonDto>> GetByIdAsync(Guid polygonId)
    {
        var polygon = await _polygonService.GetByIdAsync(polygonId);
        return Ok(polygon);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync(PolygonDto polygon)
    {
        var id = await _polygonService.CreateAsync(polygon);
        return Ok(id);
    }
}
