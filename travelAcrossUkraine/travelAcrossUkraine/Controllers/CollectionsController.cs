using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Services;
using TravelAcrossUkraine.WebApi.Utility;

namespace TravelAcrossUkraine.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CollectionsController : ControllerBase
{
    private readonly ICollectionService _collectionService;
    private readonly ILogger<CollectionsController> _logger;

    public CollectionsController(ICollectionService collectionService, ILogger<CollectionsController> logger)
    {
        _collectionService = collectionService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<CollectionDto>>> GetListAsync()
    {
        try
        {
            return await _collectionService.GetListAsync();
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    [HttpGet("addlocation")]
    public async Task<ActionResult> AddLocationToCollectionAsync(Guid collectionId, Guid locationId)
    {
        try
        {
            await _collectionService.AddLocationToCollectionsAsync(collectionId, locationId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateCollectionAsync([FromBody] CreateCollectionDto createCollectionDto)
    {
        try
        {
            return await _collectionService.CreateAsync(createCollectionDto);
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Guid>> UpdateCollectionAsync(Guid id, [FromBody] CreateCollectionDto createCollectionDto)
    {
        try
        {
            return await _collectionService.UpdateAsync(id, createCollectionDto);
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCollectionAsync(Guid id)
    {
        try
        {
            await _collectionService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }


}
