using Microsoft.EntityFrameworkCore;
using TravelAcrossUkraine.WebApi.Context;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.Repositories;

public interface ITypeRepository
{
    Task<List<TypeEntity>> GetAllAsync();
    Task<TypeEntity> GetByIdAsync(Guid id);
    Task CreateAsync(TypeEntity type);
    Task DeleteAsync(TypeEntity type);
}

public class TypeRepository : ITypeRepository
{
    public async Task CreateAsync(TypeEntity type)
    {
        var context = new TravelAcrossUkraineContext();

        context.Add(type);

        await context.SaveChangesAsync();
    }

    public async Task<List<TypeEntity>> GetAllAsync()
    {
        var context = new TravelAcrossUkraineContext();
        return await context.Types
            .Include(type => type.Categories)
            .ToListAsync();
    }

    public async Task<TypeEntity> GetByIdAsync(Guid id)
    {
        var context = new TravelAcrossUkraineContext();
        return await context.Types
            .Where(type => type.Id.Equals(id))
            .Include(type => type.Categories)
            .FirstOrDefaultAsync();
    }

    public async Task DeleteAsync(TypeEntity type)
    {
        var context = new TravelAcrossUkraineContext();

        context.Entry(type).State = EntityState.Deleted;

        await context.SaveChangesAsync();
    }
}
