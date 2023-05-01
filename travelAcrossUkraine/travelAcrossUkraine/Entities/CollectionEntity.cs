using TravelAcrossUkraine.WebApi.Entities.BaseEntities;

namespace TravelAcrossUkraine.WebApi.Entities;

public class CollectionEntity : BaseEntity
{
    public string Name { get; set; }
    public UserEntity User { get; set; }
    public Guid UserId { get; set; }
    public List<CollectionLocationEntity> CollectionLocations { get; set; }
}
