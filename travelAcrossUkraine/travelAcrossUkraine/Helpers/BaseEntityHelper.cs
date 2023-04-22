using TravelAcrossUkraine.WebApi.Entities.BaseEntities;

namespace TravelAcrossUkraine.WebApi.Helpers;

public static class BaseEntityHelper
{
    public static void SetBaseProperties<T>(T entity, DateTime? createdUpdatedDate = null) where T : BaseEntity, new()
    {
        entity ??= new T();

        entity.Id = Guid.NewGuid();
        entity.CreatedDate = createdUpdatedDate ?? DateTime.UtcNow;
        entity.UpdatedDate = createdUpdatedDate ?? DateTime.UtcNow;
    }

    public static void UpdateBaseProperties<T>(T entity, DateTime? createdUpdatedDate = null) where T : BaseEntity, new()
    {
        entity ??= new T();

        entity.UpdatedDate = createdUpdatedDate ?? DateTime.UtcNow;
    }
}
