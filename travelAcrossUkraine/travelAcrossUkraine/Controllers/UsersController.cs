using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Services;
using TravelAcrossUkraine.WebApi.Utility;

namespace TravelAcrossUkraine.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserService userService, ILogger<UsersController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetListAsync()
    {
        try
        {
            return await _userService.GetAllAsync();
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> GetListAsync(Guid id)
    {
        try
        {
            await _userService.DeleteAsync(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }
}
