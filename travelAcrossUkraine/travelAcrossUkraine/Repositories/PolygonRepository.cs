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
    private readonly TravelAcrossUkraineContext _context;

    public PolygonRepository(TravelAcrossUkraineContext context)
    {
        _context = context;
    }


    public async Task CreateAsync(PolygonEntity polygon)
    {
        _context.Add(polygon);

        await _context.SaveChangesAsync();
    }

    public async Task<List<PolygonEntity>> GetAllAsync()
    {
       return await _context.Polygons
            .Include(polygon => polygon.GeoPoints)
            .ToListAsync();
    }

    public async Task<PolygonEntity> GetByIdAsync(Guid id)
    {
        return await _context.Polygons
            .Where(polygon => polygon.Id.Equals(id))
            .Include(polygon => polygon.GeoPoints)
            .FirstOrDefaultAsync();
    }

    public async Task DeleteAsync(PolygonEntity polygon)
    {
        _context.Entry(polygon).State = EntityState.Deleted;

        await _context.SaveChangesAsync();
    }

}
