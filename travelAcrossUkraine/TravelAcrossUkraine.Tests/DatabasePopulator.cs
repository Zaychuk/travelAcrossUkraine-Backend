using TravelAcrossUkraine.WebApi.Context;
using TravelAcrossUkraine.WebApi.Entities;
using TravelAcrossUkraine.WebApi.Entities.BaseEntities;

namespace TravelAcrossUkraine.Tests;

public static class DatabasePopulator
{
    public static void EnsureFreshDatabase(TravelAcrossUkraineContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }


    public static async Task<CategoryEntity> CreateCategotyAsync(TravelAcrossUkraineContext context, Guid categoryId, Guid typeId, string name)
    {

        var category = context.Categories.FirstOrDefault(p => p.Id == categoryId);
        if (category == null)
        {
            category = new CategoryEntity
            {
                Id = categoryId,
                Name = name,
                TypeId = typeId,
            };

            await SaveEntityAsync(context, category);
        }

        return category;
    }

    public static async Task<TypeEntity> CreateTypeAsync(TravelAcrossUkraineContext context, Guid typeId, string name)
    {

        var type = context.Types.FirstOrDefault(p => p.Id == typeId);
        if (type == null)
        {
            type = new TypeEntity
            {
                Id = typeId,
                Name = name,
            };

            await SaveEntityAsync(context, type);
        }

        return type;
    }

    public static async Task<CollectionEntity> CreateCollectionAsync(TravelAcrossUkraineContext context, Guid collectionId, string name)
    {

        var collection = context.Collections.FirstOrDefault(p => p.Id == collectionId);
        if (collection == null)
        {
            collection = new CollectionEntity
            {
                Id = collectionId,
                Name = name,
            };

            await SaveEntityAsync(context, collection);
        }

        return collection;
    }


    private static async Task SaveEntityAsync(TravelAcrossUkraineContext context, BaseEntity entity)
    {
        await context.AddAsync(entity);

        await context.SaveChangesAsync();
    }
}
