namespace TravelAcrossUkraine.WebApi.Entities;

public class PolygonEntity : BaseEntity
{
    public List<GeoPointEntity> GeoPoints { get; set; }
}
