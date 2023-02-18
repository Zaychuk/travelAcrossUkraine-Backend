namespace TravelAcrossUkraine.WebApi.Dtos;

public class CircleDto
{
    public Guid Id { get; set; }
    public GeoPointDto CenterGeoPoint { get; set; }
    public decimal Radius { get; set; }
}
