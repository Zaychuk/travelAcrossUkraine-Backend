using Microsoft.EntityFrameworkCore;
using TravelAcrossUkraine.WebApi.Context;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.Repositories;

public interface ILocationRepository
{
    Task<List<LocationEntity>> GetAllAsync();
    Task<LocationEntity> GetByIdAsync(Guid id);
    Task CreateAsync(LocationEntity circle);
    Task DeleteAsync(LocationEntity location);
}

public class LocationRepository : ILocationRepository
{
    private readonly TravelAcrossUkraineContext _context;

    public LocationRepository(TravelAcrossUkraineContext context)
    {
        _context = context;
    }


    public async Task CreateAsync(LocationEntity location)
    {
        _context.Locations.Add(location);

        await _context.SaveChangesAsync();
    }

    public async Task<List<LocationEntity>> GetAllAsync()
    {
        return await _context.Locations
            .Include(location => location.Images)
            .Include(location => location.Category).ThenInclude(category => category.Type)
            .Include(location => location.GeoPoint)
            .Include(location => location.Polygon).ThenInclude(polygon => polygon.GeoPoints)
            .Include(location => location.Circle).ThenInclude(circle => circle.CenterGeoPoint)
            .ToListAsync();
    }

    public async Task<LocationEntity> GetByIdAsync(Guid id)
    {
        return await _context.Locations
            .Where(polygon => polygon.Id.Equals(id))
            .Include(location => location.Images)
            .Include(location => location.Category).ThenInclude(category => category.Type)
            .Include(location => location.GeoPoint)
            .Include(location => location.Polygon).ThenInclude(polygon => polygon.GeoPoints)
            .Include(location => location.Circle).ThenInclude(circle => circle.CenterGeoPoint)
            .FirstOrDefaultAsync();
    }

    public async Task DeleteAsync(LocationEntity location)
    {
        _context.Entry(location).State = EntityState.Deleted;

        await _context.SaveChangesAsync();
    }
}
