using Microsoft.EntityFrameworkCore;
using TravelAcrossUkraine.WebApi.Context;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.Repositories;

public interface IPolygonRepository
{
    Task<List<PolygonEntity>> GetAllAsync();
    Task<PolygonEntity> GetByIdAsync(Guid polygonId);
    Task CreateAsync(PolygonEntity polygon);
    Task DeleteAsync(PolygonEntity polygon);
}

public class PolygonRepository : IPolygonRepository
{
    public async Task CreateAsync(PolygonEntity polygon)
    {
        using var context = new TravelAcrossUkraineContext();

        context.Add(polygon);

        await context.SaveChangesAsync();
    }

    public async Task<List<PolygonEntity>> GetAllAsync()
    {
        using var context = new TravelAcrossUkraineContext();

        return await context.Polygons
            .Include(polygon => polygon.GeoPoints)
            .ToListAsync();
    }

    public async Task<PolygonEntity> GetByIdAsync(Guid id)
    {
        using var context = new TravelAcrossUkraineContext();

        return await context.Polygons
            .Where(polygon => polygon.Id.Equals(id))
            .Include(polygon => polygon.GeoPoints)
            .FirstOrDefaultAsync();
    }

    public async Task DeleteAsync(PolygonEntity polygon)
    {
        var context = new TravelAcrossUkraineContext();

        context.Entry(polygon).State = EntityState.Deleted;

        await context.SaveChangesAsync();
    }

}
