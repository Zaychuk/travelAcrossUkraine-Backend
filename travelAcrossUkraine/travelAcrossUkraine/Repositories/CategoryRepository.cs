using Microsoft.EntityFrameworkCore;
using TravelAcrossUkraine.WebApi.Context;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.Repositories;

public interface ICategoryRepository
{
    Task<List<CategoryEntity>> GetAllAsync();
    Task<CategoryEntity> GetByIdAsync(Guid id);
    Task CreateAsync(CategoryEntity category);
    Task DeleteAsync(CategoryEntity category);
    Task UpdateAsync(CategoryEntity category);
}

public class CategoryRepository : ICategoryRepository
{
    private readonly TravelAcrossUkraineContext _context;

    public CategoryRepository(TravelAcrossUkraineContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(CategoryEntity category)
    {
        _context.Add(category);

        await _context.SaveChangesAsync();
    }

    public async Task<List<CategoryEntity>> GetAllAsync()
    {
        return await _context.Categories
            .Include(category => category.Type)
            .ToListAsync();
    }

    public async Task<CategoryEntity> GetByIdAsync(Guid id)
    {
        return await _context.Categories
            .Where(category => category.Id.Equals(id))
            .Include(category => category.Type)
            .FirstOrDefaultAsync();
    }


    public async Task UpdateAsync(CategoryEntity category)
    {
        _context.Categories.Update(category);

        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(CategoryEntity category)
    {
        category.IsDeleted = true;
        _context.Entry(category).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }
}
