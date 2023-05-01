using TravelAcrossUkraine.WebApi.Entities.BaseEntities;

namespace TravelAcrossUkraine.WebApi.Entities;

public class LocationEntity : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ImageEntity> Images { get; set; }
    public string? WikipediaUrl { get; set; }
    public string? PetitionUrl { get; set; }
    public CategoryEntity Category { get; set; }
    public Guid CategoryId { get; set; }
    public PolygonEntity? Polygon { get; set; }
    public Guid? PolygonId { get; set; }
    public GeoPointEntity? GeoPoint { get; set; }
    public Guid? GeoPointId { get; set; }
    public CircleEntity? Circle { get; set; }
    public Guid? CircleId { get; set; }
    public List<CollectionLocationEntity> CollectionLocations { get; set; }
}
