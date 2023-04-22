using Microsoft.EntityFrameworkCore;
using TravelAcrossUkraine.WebApi.Context;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.Repositories;

public interface IGeoPointRepository
{
    Task<List<GeoPointEntity>> GetAllAsync();
    Task<GeoPointEntity> GetByIdAsync(Guid id);
    Task CreateAsync(GeoPointEntity geoPoint);
    Task DeleteAsync(GeoPointEntity geoPoint);
}

public class GeoPointRepository : IGeoPointRepository
{
    public async Task CreateAsync(GeoPointEntity geoPoint)
    {
        using var context = new TravelAcrossUkraineContext();

        context.Entry(geoPoint).State = EntityState.Added;

        await context.SaveChangesAsync();
    }

    public async Task<List<GeoPointEntity>> GetAllAsync()
    {
        using var context = new TravelAcrossUkraineContext();

        return await context.GeoPoints
            .ToListAsync();
    }

    public async Task<GeoPointEntity> GetByIdAsync(Guid id)
    {
        using var context = new TravelAcrossUkraineContext();
        return await context.GeoPoints
            .Where(geoPoint => geoPoint.Id.Equals(id))
            .FirstOrDefaultAsync();
    }

    public async Task DeleteAsync(GeoPointEntity geoPoint)
    {
        var context = new TravelAcrossUkraineContext();

        context.Entry(geoPoint).State = EntityState.Deleted;

        await context.SaveChangesAsync();
    }
}
