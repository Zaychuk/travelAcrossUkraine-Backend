namespace TravelAcrossUkraine.WebApi.Dtos;

public class PolygonDto
{
    public Guid Id { get; set; }
    public List<GeoPointDto> GeoPoints { get; set; }
}
