using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using TravelAcrossUkraine.WebApi.Context;
using TravelAcrossUkraine.WebApi.Entities;
using TravelAcrossUkraine.WebApi.Utility.Enums;

namespace TravelAcrossUkraine.WebApi.Repositories;

public interface ICollectionRepository
{
    Task AddLocationToCollectionAsync(CollectionLocationEntity collectionLocation);
    Task CreateAsync(CollectionEntity collectionLocation);
    Task DeleteAsync(CollectionEntity collectionLocation);
    Task<CollectionEntity> GetByIdAsync(Guid id);
    Task<List<CollectionEntity>> GetListAsync(Guid userId);
    Task UpdateAsync(CollectionEntity collectionLocation);
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

    public async Task AddLocationToCollectionAsync(CollectionLocationEntity collectionLocation)
    {
        _context.CollectionLocation.Add(collectionLocation);

        await _context.SaveChangesAsync();
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
}
