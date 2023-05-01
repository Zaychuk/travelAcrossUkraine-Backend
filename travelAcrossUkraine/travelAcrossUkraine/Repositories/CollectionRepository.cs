using TravelAcrossUkraine.WebApi.Context;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.Repositories;

public interface ICollectionRepository
{
    Task AddLocationToCollectionAsync(CollectionLocationEntity collectionLocation);
}

public class CollectionRepository : ICollectionRepository
{
    private readonly TravelAcrossUkraineContext _context;

    public CollectionRepository(TravelAcrossUkraineContext context)
    {
        _context = context;
    }

    public async Task AddLocationToCollectionAsync(CollectionLocationEntity collectionLocation)
    {
        _context.CollectionLocation.Add(collectionLocation);

        await _context.SaveChangesAsync();
    }
}
