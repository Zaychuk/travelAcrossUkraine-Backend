using Microsoft.EntityFrameworkCore;
using TravelAcrossUkraine.WebApi.Context;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.Repositories;

public interface ITypeRepository
{
    Task<List<TypeEntity>> GetAllAsync();
    Task<TypeEntity> GetByIdAsync(Guid id);
    Task CreateAsync(TypeEntity type);
}

public class TypeRepository : ITypeRepository
{
    public async Task CreateAsync(TypeEntity type)
    {
        var context = new TravelAcrossUkraineContext();

        type.CreatedDate = DateTime.Now;
        type.UpdatedDate = DateTime.Now;

        context.Add(type);

        await context.SaveChangesAsync();
    }

    public async Task<List<TypeEntity>> GetAllAsync()
    {
        var context = new TravelAcrossUkraineContext();
        return await context.Types
            .Where(type => !type.IsDeleted)
            .ToListAsync();
    }

    public async Task<TypeEntity> GetByIdAsync(Guid id)
    {
        var context = new TravelAcrossUkraineContext();
        return await context.Types
            .Where(type => !type.IsDeleted && type.Id.Equals(id))
            .SingleAsync();
    }
}
