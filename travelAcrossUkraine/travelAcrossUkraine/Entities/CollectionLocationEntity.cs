using TravelAcrossUkraine.WebApi.Entities.BaseEntities;

namespace TravelAcrossUkraine.WebApi.Entities;

public class CollectionLocationEntity : BaseEntity
{
    public CollectionEntity Collection { get; set; }
    public Guid CollectionId { get; set; }
    public LocationEntity Location { get; set; }
    public Guid LocationId { get; set; }
}
