namespace TravelAcrossUkraine.WebApi.Dtos;

public class CreateLocationDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<IFormFile> ImageFiles { get; set; }
    public string? WikipediaUrl { get; set; }
    public string? PetitionUrl { get; set; }
    public Guid CategoryId { get; set; }
    public GeoPointDto? GeoPoint { get; set; }
    public PolygonDto? Polygon { get; set; }
    public CircleDto? Circle { get; set; }
}
