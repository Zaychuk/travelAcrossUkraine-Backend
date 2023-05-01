using System.Security.Claims;
using TravelAcrossUkraine.WebApi.Entities;
using TravelAcrossUkraine.WebApi.Exceptions;
using TravelAcrossUkraine.WebApi.Helpers;
using TravelAcrossUkraine.WebApi.Repositories;

namespace TravelAcrossUkraine.WebApi.Services;

public interface ICollectionService
{
    Task AddLocationToCollectionAsync(Guid collectionId, Guid locationId);
}

public class CollectionService : ICollectionService
{
    private readonly ICollectionRepository _collectionRepository;
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CollectionService(ICollectionRepository collectionRepository, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _collectionRepository = collectionRepository;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task AddLocationToCollectionAsync(Guid collectionId, Guid locationId)
    {
        var identity = _httpContextAccessor?.HttpContext?.User?.Identity as ClaimsIdentity ?? throw new UnauthorizedAccessException();
        var userClaims = identity.Claims;

        var authenticatedUser = await _userRepository.GetAsync(userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

        if (authenticatedUser == null || !authenticatedUser.Collections.Any(c => c.Id == collectionId))
        {
            throw new ForbiddenException("User doesn`t have an access to modify this collection");
        }

        var collectionLocation = new CollectionLocationEntity
        {
            LocationId = locationId,
            CollectionId = collectionId
        };
        BaseEntityHelper.SetBaseProperties(collectionLocation);

        await _collectionRepository.AddLocationToCollectionAsync(collectionLocation);
    }
}
