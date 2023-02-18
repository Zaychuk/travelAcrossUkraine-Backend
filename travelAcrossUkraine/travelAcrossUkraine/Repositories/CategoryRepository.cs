using Microsoft.EntityFrameworkCore;
using TravelAcrossUkraine.WebApi.Context;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.Repositories;

public interface ICategoryRepository
{
    Task<List<CategoryEntity>> GetAllAsync();
    Task<CategoryEntity> GetByIdAsync(Guid id);
    Task CreateAsync(CategoryEntity category);
}

public class CategoryRepository : ICategoryRepository
{
    public async Task CreateAsync(CategoryEntity category)
    {
        var context = new TravelAcrossUkraineContext();

        category.CreatedDate = DateTime.UtcNow;
        category.UpdatedDate = DateTime.UtcNow;

        context.Add(category);

        await context.SaveChangesAsync();
    }

    public async Task<List<CategoryEntity>> GetAllAsync()
    {
        var context = new TravelAcrossUkraineContext();

        return await context.Categories
            .Where(category => !category.IsDeleted)
            .Include(category => category.Type)
            .ToListAsync();
    }

    public async Task<CategoryEntity> GetByIdAsync(Guid id)
    {
        var context = new TravelAcrossUkraineContext();

        return await context.Categories
            .Where(category => !category.IsDeleted && category.Id.Equals(id))
            .Include(category => category.Type)
            .SingleAsync();
    }
}
