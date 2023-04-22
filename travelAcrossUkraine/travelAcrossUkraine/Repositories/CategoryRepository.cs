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
}

public class CategoryRepository : ICategoryRepository
{
    public async Task CreateAsync(CategoryEntity category)
    {
        var context = new TravelAcrossUkraineContext();

        context.Add(category);

        await context.SaveChangesAsync();
    }

    public async Task<List<CategoryEntity>> GetAllAsync()
    {
        var context = new TravelAcrossUkraineContext();

        return await context.Categories
            .Include(category => category.Type)
            .ToListAsync();
    }

    public async Task<CategoryEntity> GetByIdAsync(Guid id)
    {
        var context = new TravelAcrossUkraineContext();

        return await context.Categories
            .Where(category => category.Id.Equals(id))
            .Include(category => category.Type)
            .FirstOrDefaultAsync();
    }


    public async Task DeleteAsync(CategoryEntity category)
    {
        var context = new TravelAcrossUkraineContext();

        context.Entry(category).State = EntityState.Deleted;

        await context.SaveChangesAsync();
    }
}
