using Microsoft.EntityFrameworkCore;
using TravelAcrossUkraine.WebApi.Context;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.Repositories;

public interface ILocationRepository
{
    Task<List<LocationEntity>> GetAllAsync();
    Task<LocationEntity> GetByIdAsync(Guid id);
    Task CreateAsync(LocationEntity circle);
}

public class LocationRepository : ILocationRepository
{
    public async Task CreateAsync(LocationEntity location)
    {
        using var context = new TravelAcrossUkraineContext();
        location.CreatedDate = DateTime.UtcNow;
        location.UpdatedDate = DateTime.UtcNow;

        context.Entry(location).State = EntityState.Added;
        location.Images.ForEach(image =>
        {
            image.CreatedDate = DateTime.UtcNow;
            image.UpdatedDate = DateTime.UtcNow;
            context.Entry(image).State = EntityState.Added;
        });

        await context.SaveChangesAsync();
    }

    public async Task<List<LocationEntity>> GetAllAsync()
    {
        using var context = new TravelAcrossUkraineContext();

        return await context.Locations
            .Where(location => !location.IsDeleted)
            .Include(location => location.Images)
            .Include(location => location.Category).ThenInclude(category => category.Type)
            .ToListAsync();
    }

    public async Task<LocationEntity> GetByIdAsync(Guid id)
    {
        using var context = new TravelAcrossUkraineContext();

        return await context.Locations
            .Where(polygon => polygon.Id.Equals(id) && !polygon.IsDeleted)
            .Include(location => location.Images)
            .Include(location => location.Category).ThenInclude(category => category.Type)
            .SingleAsync();
    }
}
