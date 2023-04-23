using Microsoft.EntityFrameworkCore;
using TravelAcrossUkraine.WebApi.Context;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.Repositories;

public interface ICircleRepository
{
    Task<List<CircleEntity>> GetAllAsync();
    Task<CircleEntity> GetByIdAsync(Guid id);
    Task CreateAsync(CircleEntity circle);
    Task DeleteAsync(CircleEntity circle);
}

public class CircleRepository : ICircleRepository
{
    private readonly TravelAcrossUkraineContext _context;

    public CircleRepository(TravelAcrossUkraineContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(CircleEntity circle)
    {
        _context.Circles.Add(circle);

        await _context.SaveChangesAsync();
    }

    public async Task<List<CircleEntity>> GetAllAsync()
    {
        return await _context.Circles
            .Include(circle => circle.CenterGeoPoint)
            .ToListAsync();
    }

    public async Task<CircleEntity> GetByIdAsync(Guid id)
    {
        return await _context.Circles
            .Where(circle => circle.Id.Equals(id))
            .Include(circle => circle.CenterGeoPoint)
            .FirstOrDefaultAsync();
    }

    public async Task DeleteAsync(CircleEntity circle)
    {
        _context.Entry(circle).State = EntityState.Deleted;

        await _context.SaveChangesAsync();
    }
}
