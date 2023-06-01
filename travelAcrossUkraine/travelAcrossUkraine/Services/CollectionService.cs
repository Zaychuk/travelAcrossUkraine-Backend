using AutoMapper;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Entities;
using TravelAcrossUkraine.WebApi.Exceptions;
using TravelAcrossUkraine.WebApi.Helpers;
using TravelAcrossUkraine.WebApi.Repositories;

namespace TravelAcrossUkraine.WebApi.Services;

public interface ICollectionService
{
    Task AddLocationToCollectionsAsync(List<Guid> collectionIds, Guid locationId);
    Task<Guid> CreateAsync(CreateCollectionDto categoryDto);
    Task<Guid> DeleteAsync(Guid id);
    Task<List<CollectionDto>> GetListAsync();
    Task<Guid> UpdateAsync(Guid id, CreateCollectionDto categoryDto);
}

public class CollectionService : ICollectionService
{
    private readonly ICollectionRepository _collectionRepository;
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public CollectionService(ICollectionRepository collectionRepository, IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _collectionRepository = collectionRepository;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task<List<CollectionDto>> GetListAsync()
    {
        var authenticatedUser = AuthenticatedUserHelper.GetAuthenticatedUser(_httpContextAccessor?.HttpContext?.User?.Identity);
        var collections = await _collectionRepository.GetListAsync(authenticatedUser.Id);

        return collections
            .Select(collection => _mapper.Map<CollectionDto>(collection))
            .ToList();
    }
    public async Task AddLocationToCollectionsAsync(List<Guid> collectionIds, Guid locationId)
    {

        var authenticatedUser = AuthenticatedUserHelper.GetAuthenticatedUser(_httpContextAccessor?.HttpContext?.User?.Identity);

        var user = await _userRepository.GetAsync(authenticatedUser.Username);
        var userCollectionIds = user.Collections.Select(c => c.Id).ToList();

        if (user == null || collectionIds.Any(c => !userCollectionIds.Contains(c)))
        {
            throw new ForbiddenException("User doesn`t have an access to modify this collection");
        }
        var existedCollectionLocations = await _collectionRepository.GetCollectionLocationsByLocationIdAsync(locationId);
        var existedCollectionLocationsCollections = existedCollectionLocations.Select(cl => cl.CollectionId).ToList();

        var collectionLocationsToCreate = collectionIds.Where(c => !existedCollectionLocationsCollections.Contains(c)).Select(collectionId =>
        {
            var collectionLocation = new CollectionLocationEntity
            {
                LocationId = locationId,
                CollectionId = collectionId
            };
            BaseEntityHelper.SetBaseProperties(collectionLocation);

            return collectionLocation;
        }).ToList();

        var collectionLocationsToDelete = existedCollectionLocationsCollections.Where(c => !collectionIds.Contains(c)).Select(c =>
        {
            var collectionToDelete = existedCollectionLocations.Single(exl => exl.Collection.Id == c);
            collectionToDelete.IsDeleted = true;
            BaseEntityHelper.UpdateBaseProperties(collectionToDelete);
            return collectionToDelete;
        }).ToList();

        if (collectionLocationsToCreate.Any())
        {
            await _collectionRepository.AddLocationToCollectionsAsync(collectionLocationsToCreate);
        }
        if (collectionLocationsToDelete.Any())
        {
            await _collectionRepository.RemoveCollectionLocationsAsync(collectionLocationsToDelete);
        }
    }

    public async Task<Guid> CreateAsync(CreateCollectionDto categoryDto)
    {
        var authenticatedUser = AuthenticatedUserHelper.GetAuthenticatedUser(_httpContextAccessor?.HttpContext?.User?.Identity);
        var collection = _mapper.Map<CollectionEntity>(categoryDto);
        collection.UserId = authenticatedUser.Id;
        BaseEntityHelper.SetBaseProperties(collection);

        await _collectionRepository.CreateAsync(collection);

        return collection.Id;
    }

    public async Task<Guid> UpdateAsync(Guid id, CreateCollectionDto categoryDto)
    {
        var collection = await _collectionRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Collection with id {id} not found");
        collection = _mapper.Map(categoryDto, collection);
        BaseEntityHelper.UpdateBaseProperties(collection);

        await _collectionRepository.UpdateAsync(collection);

        return collection.Id;
    }

    public async Task<Guid> DeleteAsync(Guid id)
    {
        var collection = await _collectionRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Collection with id {id} not found");
        BaseEntityHelper.UpdateBaseProperties(collection);

        await _collectionRepository.DeleteAsync(collection);

        return collection.Id;
    }
}
