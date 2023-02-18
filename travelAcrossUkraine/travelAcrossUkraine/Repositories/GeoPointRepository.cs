using Microsoft.EntityFrameworkCore;
using TravelAcrossUkraine.WebApi.Context;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.Repositories;

public interface IGeoPointRepository
{
    Task<List<GeoPointEntity>> GetAllAsync();
    Task<GeoPointEntity> GetByIdAsync(Guid id);
    Task CreateAsync(GeoPointEntity geoPoint);
}

public class GeoPointRepository : IGeoPointRepository
{
    public async Task CreateAsync(GeoPointEntity geoPoint)
    {
        using var context = new TravelAcrossUkraineContext();
        geoPoint.CreatedDate = DateTime.UtcNow;
        geoPoint.UpdatedDate = DateTime.UtcNow;
        await context.GeoPoints.AddAsync(geoPoint);
        await context.SaveChangesAsync();
    }

    public async Task<List<GeoPointEntity>> GetAllAsync()
    {
        using var context = new TravelAcrossUkraineContext();

        return await context.GeoPoints
            .Where(geoPoint => !geoPoint.IsDeleted)
            .ToListAsync();
    }

    public async Task<GeoPointEntity> GetByIdAsync(Guid id)
    {
        using var context = new TravelAcrossUkraineContext();
        return await context.GeoPoints
            .Where(geoPoint => geoPoint.Id.Equals(id) && !geoPoint.IsDeleted)
            .SingleAsync();
    }
}
