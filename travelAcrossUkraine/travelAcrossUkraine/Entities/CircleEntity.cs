namespace TravelAcrossUkraine.WebApi.Entities;

public class CircleEntity : BaseEntity
{
    public GeoPointEntity CenterGeoPoint { get; set; }
    public Guid CenterGeoPointId { get; set; }
    public decimal Radius { get; set; }
}
