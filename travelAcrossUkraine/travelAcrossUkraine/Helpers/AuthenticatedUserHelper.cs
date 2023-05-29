using System.Security.Claims;
using System.Security.Principal;
using TravelAcrossUkraine.WebApi.Dtos;

namespace TravelAcrossUkraine.WebApi.Helpers;

public static class AuthenticatedUserHelper
{
    public static UserDto GetAuthenticatedUser(IIdentity? httpIdentiny)
    {
        var identity = httpIdentiny as ClaimsIdentity ?? throw new UnauthorizedAccessException();

        var userClaims = identity.Claims;

        return new UserDto
        {
            EmailAddress = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
            GivenName = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value,
            Surname = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value,
            Role = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
            Id = Guid.Parse(userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value),
            Username = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
        };
    }
}
