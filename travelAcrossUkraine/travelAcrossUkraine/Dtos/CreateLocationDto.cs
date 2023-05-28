namespace TravelAcrossUkraine.WebApi.Dtos;

public class CreateLocationDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> ImageUrls { get; set; }
    public string? WikipediaUrl { get; set; }
    public string? PetitionUrl { get; set; }
    public Guid CategoryId { get; set; }
    public List<Guid> CollectionId { get; set; }
    public CreateGeoPointDto? GeoPoint { get; set; }
    public CreatePolygonDto? Polygon { get; set; }
    public CreateCircleDto? Circle { get; set; }
}
