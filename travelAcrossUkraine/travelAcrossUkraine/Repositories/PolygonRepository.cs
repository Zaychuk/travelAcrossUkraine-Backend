using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using travelAcrossUkraine.WebApi.Context;
using travelAcrossUkraine.WebApi.Entities;

namespace travelAcrossUkraine.WebApi.Repositories;

public interface IPolygonRepository
{
    Task<List<PolygonEntity>> GetAllAsync();
    Task<PolygonEntity> GetByIdAsync(Guid polygonId);
    Task CreateAsync(PolygonEntity polygon);
}

public class PolygonRepository : IPolygonRepository
{
    public async Task CreateAsync(PolygonEntity polygon)
    {
        using var context = new TravelAcrossUkraineContext();
        polygon.CreatedDate = DateTime.UtcNow;
        polygon.UpdatedDate = DateTime.UtcNow;

        context.Entry(polygon).State = EntityState.Added;
        polygon.GeoPoints.ForEach(geoPoint => {
            geoPoint.CreatedDate = DateTime.UtcNow;
            geoPoint.UpdatedDate = DateTime.UtcNow;
            context.Entry(geoPoint).State = EntityState.Added;
            });

        await context.SaveChangesAsync();
    }

    public async Task<List<PolygonEntity>> GetAllAsync()
    {
        using var context = new TravelAcrossUkraineContext();

        return await context.Polygons
            .Where(polygon => !polygon.IsDeleted)
            .Include(polygon => polygon.GeoPoints)
            .ToListAsync();
    }

    public async Task<PolygonEntity> GetByIdAsync(Guid id)
    {
        using var context = new TravelAcrossUkraineContext();

        return await context.Polygons
            .Where(polygon => polygon.Id.Equals(id) && !polygon.IsDeleted)
            .Include(polygon => polygon.GeoPoints)
            .SingleAsync();
    }

}
