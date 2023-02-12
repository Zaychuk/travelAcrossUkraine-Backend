using travelAcrossUkraine.WebApi.Enums;

namespace travelAcrossUkraine.WebApi.Entities;

public class GeoPointEntity : BaseEntity
{
    public decimal CoordinateX { get; set; }
    public decimal CoordinateY { get; set; }
    public int? SequenceNumber { get; set; }
    public GeoPointTypes Type { get; set; }
    public Guid? PolygonId { get; set; }
    public PolygonEntity Polygon { get; set; }
}
