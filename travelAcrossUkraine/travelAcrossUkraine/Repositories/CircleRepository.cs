using Microsoft.EntityFrameworkCore;
using TravelAcrossUkraine.WebApi.Context;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.Repositories;

public interface ICircleRepository
{
    Task<List<CircleEntity>> GetAllAsync();
    Task<CircleEntity> GetByIdAsync(Guid id);
    Task CreateAsync(CircleEntity circle);
}

public class CircleRepository : ICircleRepository
{
    public async Task CreateAsync(CircleEntity circle)
    {
        var context = new TravelAcrossUkraineContext();
        circle.CreatedDate = DateTime.Now;
        circle.UpdatedDate = DateTime.Now;
        circle.CenterGeoPoint.CreatedDate = DateTime.Now;
        circle.CenterGeoPoint.UpdatedDate = DateTime.Now;

        context.Circles.Add(circle);

        await context.SaveChangesAsync();
    }

    public async Task<List<CircleEntity>> GetAllAsync()
    {
        var context = new TravelAcrossUkraineContext();

        return await context.Circles
            .Where(circle => !circle.IsDeleted)
            .Include(circle => circle.CenterGeoPoint)
            .ToListAsync();
    }

    public async Task<CircleEntity> GetByIdAsync(Guid id)
    {
        var context = new TravelAcrossUkraineContext();

        return await context.Circles
            .Where(circle => !circle.IsDeleted && circle.Id.Equals(id))
            .Include(circle => circle.CenterGeoPoint)
            .SingleAsync();
    }
}
