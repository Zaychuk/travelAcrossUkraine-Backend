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
    private readonly TravelAcrossUkraineContext _context;

    public TypeRepository(TravelAcrossUkraineContext context)
    {
        _context = context;
    }


    public async Task CreateAsync(TypeEntity type)
    {
        _context.Add(type);

        await _context.SaveChangesAsync();
    }

    public async Task<List<TypeEntity>> GetAllAsync()
    {
        return await _context.Types
            .Include(type => type.Categories)
            .ToListAsync();
    }

    public async Task<TypeEntity> GetByIdAsync(Guid id)
    {
        return await _context.Types
            .Where(type => type.Id.Equals(id))
            .Include(type => type.Categories)
            .FirstOrDefaultAsync();
    }

    public async Task DeleteAsync(TypeEntity type)
    {
        _context.Entry(type).State = EntityState.Deleted;

        await _context.SaveChangesAsync();
    }
}
