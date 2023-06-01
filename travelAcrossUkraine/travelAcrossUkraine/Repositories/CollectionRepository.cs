using Microsoft.EntityFrameworkCore;
using TravelAcrossUkraine.WebApi.Context;
using TravelAcrossUkraine.WebApi.Entities;
using TravelAcrossUkraine.WebApi.Utility.Enums;

namespace TravelAcrossUkraine.WebApi.Repositories;

public interface ICollectionRepository
{
    Task AddLocationToCollectionsAsync(List<CollectionLocationEntity> collectionLocations);
    Task<List<CollectionLocationEntity>> GetCollectionLocationsByLocationIdAsync(Guid locationId);
    Task<bool> CollectionLocationExistsAsync(Guid collectionId, Guid locationId);
    Task CreateAsync(CollectionEntity collectionLocation);
    Task DeleteAsync(CollectionEntity collectionLocation);
    Task<CollectionEntity> GetByIdAsync(Guid id);
    Task<List<CollectionEntity>> GetListAsync(Guid userId);
    Task UpdateAsync(CollectionEntity collectionLocation);
    Task RemoveCollectionLocationsAsync(List<CollectionLocationEntity> collectionLocations);
}

public class CollectionRepository : ICollectionRepository
{
    private readonly TravelAcrossUkraineContext _context;

    public CollectionRepository(TravelAcrossUkraineContext context)
    {
        _context = context;
    }

    public async Task<List<CollectionEntity>> GetListAsync(Guid userId)
    {
        return await _context.Collections
            .Where(collection => collection.UserId == userId)
            .Include(collection => collection.CollectionLocations.Where(collectionLocation => collectionLocation.Location.Status == LocationStatuses.Approved))
            .ThenInclude(collectionLocation => collectionLocation.Location).ThenInclude(l => l.Images)
            .ToListAsync();
    }

    public async Task<CollectionEntity> GetByIdAsync(Guid id)
    {
        return await _context.Collections
            .FirstOrDefaultAsync(collection => collection.Id == id);
    }

    public async Task AddLocationToCollectionsAsync(List<CollectionLocationEntity> collectionLocations)
    {
        _context.CollectionLocation.AddRange(collectionLocations);

        await _context.SaveChangesAsync();
    }

    public async Task RemoveCollectionLocationsAsync(List<CollectionLocationEntity> collectionLocations)
    {
        _context.CollectionLocation.RemoveRange(collectionLocations);

        await _context.SaveChangesAsync();
    }

    public async Task<bool> CollectionLocationExistsAsync(Guid collectionId, Guid locationId)
    {
        return await _context.CollectionLocation.AnyAsync(cl => cl.CollectionId == collectionId && cl.LocationId == locationId);
    }

    public async Task CreateAsync(CollectionEntity collectionLocation)
    {
        _context.Collections.Add(collectionLocation);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CollectionEntity collectionLocation)
    {
        _context.Collections.Update(collectionLocation);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(CollectionEntity collectionLocation)
    {
        _context.Collections.Remove(collectionLocation);

        await _context.SaveChangesAsync();
    }

    public async Task<List<CollectionLocationEntity>> GetCollectionLocationsByLocationIdAsync(Guid locationId)
    {
        return await _context.CollectionLocation.Where(cl => cl.LocationId == locationId)
            .ToListAsync();
    }
}
