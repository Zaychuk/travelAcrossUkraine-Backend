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
    private readonly TravelAcrossUkraineContext _context;

    public GeoPointRepository(TravelAcrossUkraineContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(GeoPointEntity geoPoint)
    {
        _context.Entry(geoPoint).State = EntityState.Added;

        await _context.SaveChangesAsync();
    }

    public async Task<List<GeoPointEntity>> GetAllAsync()
    {
        return await _context.GeoPoints
            .ToListAsync();
    }

    public async Task<GeoPointEntity> GetByIdAsync(Guid id)
    {
        return await _context.GeoPoints
            .Where(geoPoint => geoPoint.Id.Equals(id))
            .FirstOrDefaultAsync();
    }

    public async Task DeleteAsync(GeoPointEntity geoPoint)
    {
        _context.Entry(geoPoint).State = EntityState.Deleted;

        await _context.SaveChangesAsync();
    }
}
