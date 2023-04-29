using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Dtos.Auth;
using TravelAcrossUkraine.WebApi.Services.Auth;
using TravelAcrossUkraine.WebApi.Utility;
using TravelAcrossUkraine.WebApi.Utility.Validators;

namespace TravelAcrossUkraine.WebApi.Controllers.Auth;

[Route("api/auth")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService loginService, ILogger<AuthController> logger)
    {
        _authService = loginService;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> LoginAsync([FromBody] UserLoginDto userLogin)
    {
        try
        {
            Validators.ValidateUserLoginDto(userLogin);

            return Ok(await _authService.AuthenticateAsync(userLogin));
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }

    [HttpPost("signUp")]
    public async Task<ActionResult> SignUpAsync([FromBody] CreateUserDto userDto)
    {
        try
        {
            Validators.ValidateCreateUserDto(userDto);

            await _authService.SignUpAsync(userDto);

            return NoContent();
        }
        catch (Exception ex)
        {
            return ExceptionHandler.Handle(ex, _logger);
        }
    }


    private UserDto GetAuthenticatedUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity ?? throw new UnauthorizedAccessException();

        var userClaims = identity.Claims;

        return new UserDto
        {
            EmailAdress = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
            GivenName = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value,
            Surname = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value,
            Role = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
            Username = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
        };
    }
}
