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
    public async Task CreateAsync(CircleEntity circle)
    {
        var context = new TravelAcrossUkraineContext();

        context.Circles.Add(circle);

        await context.SaveChangesAsync();
    }

    public async Task<List<CircleEntity>> GetAllAsync()
    {
        var context = new TravelAcrossUkraineContext();

        return await context.Circles
            .Include(circle => circle.CenterGeoPoint)
            .ToListAsync();
    }

    public async Task<CircleEntity> GetByIdAsync(Guid id)
    {
        var context = new TravelAcrossUkraineContext();

        return await context.Circles
            .Where(circle => circle.Id.Equals(id))
            .Include(circle => circle.CenterGeoPoint)
            .FirstOrDefaultAsync();
    }

    public async Task DeleteAsync(CircleEntity circle)
    {
        var context = new TravelAcrossUkraineContext();

        context.Entry(circle).State = EntityState.Deleted;

        await context.SaveChangesAsync();
    }
}
