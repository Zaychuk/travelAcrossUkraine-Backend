namespace TravelAcrossUkraine.WebApi.Dtos;

public class LocationDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ImageWithoutLocationDto> Images { get; set; }
    public string? WikipediaUrl { get; set; }
    public string? PetitionUrl { get; set; }
    public CategoryDto Category { get; set; }
    public GeoPointDto? GeoPoint { get; set; }
    public PolygonDto? Polygon { get; set; }
    public CircleDto? Circle { get; set; }
}
