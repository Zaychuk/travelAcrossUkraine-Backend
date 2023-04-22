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
    public async Task CreateAsync(LocationEntity location)
    {
        using var context = new TravelAcrossUkraineContext();

        context.Locations.Add(location);

        await context.SaveChangesAsync();
    }

    public async Task<List<LocationEntity>> GetAllAsync()
    {
        using var context = new TravelAcrossUkraineContext();

        return await context.Locations
            .Include(location => location.Images)
            .Include(location => location.Category).ThenInclude(category => category.Type)
            .Include(location => location.GeoPoint)
            .Include(location => location.Polygon).ThenInclude(polygon => polygon.GeoPoints)
            .Include(location => location.Circle).ThenInclude(circle => circle.CenterGeoPoint)
            .ToListAsync();
    }

    public async Task<LocationEntity> GetByIdAsync(Guid id)
    {
        using var context = new TravelAcrossUkraineContext();

        return await context.Locations
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
        var context = new TravelAcrossUkraineContext();

        context.Entry(location).State = EntityState.Deleted;

        await context.SaveChangesAsync();
    }
}
